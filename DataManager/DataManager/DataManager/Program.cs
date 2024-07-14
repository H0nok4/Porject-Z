using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CsvParser {
    class Program {
        //临时，等完善后打包成程序去运行的话，需要改成相对路径
        static string _csvDir = @"Table"; // 指定CSV文件所在目录
        static string _keyValueTableDir = @"KeyValueTable"; // 指定KeyValueTable文件所在目录
        static string _configTypeCSDir = @"Output\ConfigType.cs";
        static string _outputDir = @"Output"; // 指定输出目录
        static string _outputXMLDir = @"Output\xml";// 指定XML输出目录
        static string _gameXMLDir = @"..\Assets\Resources\Config\xml\";//游戏的配置目录
        static string _gameConfigTypeDir = @"..\Assets\Resources\Config\"; //游戏的配置代码目录
        static string _dataManagerScriptDir = @"..\Assets\Scripts\Config\"; //游戏的配置代码目录
        private static List<CSVFile> csvFiles = new List<CSVFile>();
        private static Dictionary<string,Type> EnumTypes = new Dictionary<string, Type>();
        static void Main(string[] args) {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                ReadAllCSV();

                //整理主表和子表
                SetMainFileAndChildren();
                //校验CSV表的主键
                VerifyCSV();

                GenerateKeyValueTable();
                //TODO:可能还得校验数据的有效性，比如所填的数据是否是对应的类型

                //先生成Type的文件
                GenerateConfigTypeScriptFile();

                //根据生成的Type文件，使用Roslyn来生成对应的List数据
                GenerateData();

                //将生成出来的代码和XML文件拷贝到游戏工程中
                CopyToGameProject();

                //开始生成一个DataManager.cs文件，用于游戏内获取所有的游戏配置
                GenerateDataManagerScriptFile();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
            Console.WriteLine("按任意键结束......");
            Console.ReadKey();
        }

        private static void ReadAllCSV()
        {
            foreach (var file in Directory.GetFiles(_csvDir, "*.csv")) {
                //需要分格名称
                var csvFileInstance = CSVFile.ReadCSVFile(file);
                csvFiles.Add(csvFileInstance);
            }
        }

        private static void SetMainFileAndChildren()
        {
            //先筛选出有主表的，然后选出主表
            List<CSVFile> hasMainFile = new List<CSVFile>();
            foreach (var csvFile in csvFiles)
            {
                var splitName = csvFile.FileName.Split("_");
                var mainFileName = splitName[0];

                if (splitName.Length > 1)
                {
                    if (csvFiles.Find((file) => file.FileName == mainFileName) is { } mainFile)
                    {
                        csvFile.Parent = mainFile;
                        mainFile.Children.Add(csvFile);
                        Console.WriteLine($"找到一个有主表的子表，主表名称为：{mainFileName},子表名称为:{csvFile.FileName}");
                    }
                    hasMainFile.Add(csvFile);
                }
            }

            foreach (var csvFile in hasMainFile)
            {
                csvFiles.Remove(csvFile);
            }
        }

        private static void GenerateKeyValueTable()
        {
            //Enum表
            GenerateEnums();
            //Localization表
            GenerateLocalization();
        }

        private static void GenerateLocalization()
        {
            //TODO:多语言默认只会填ID或者中文会生成俩个字典，一个字典以ID为主键，多语言配置为值，一个字典以中文为主键多语言配置为值
        }

        private static void GenerateEnums()
        {
            var enumTable = CSVFile.ReadCSVFile(Path.Combine(_keyValueTableDir, "Enum.csv"));
            if (enumTable == null)
            {
                Console.WriteLine("Enum表不存在");
                return;
            }

            for (int i = 4; i < enumTable.Data.Count; i++)
            {
                if (enumTable.Data[i][0] != "")
                {
                    string enumName = enumTable.Data[i][0];
                    Console.WriteLine($"生成枚举{enumName}");
                    EnumBuilder enumBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("EnumAssembly"), AssemblyBuilderAccess.Run).DefineDynamicModule("EnumModule").DefineEnum(enumTable.Data[i][0], TypeAttributes.Public, typeof(int));
                    int j = 0;
                    enumBuilder.DefineLiteral(enumTable.Data[i][1], j);
                    i++;
                    j++;
                    //从这里开始到下一个非空的位置为止全是这个枚举的值
                    if (enumTable.Data.Count <= i)
                    {
                        //最后只有一行，跳出
                        EnumTypes.Add(enumName, enumBuilder.CreateType());
                        break;
                    }
                    while (enumTable.Data[i][0] == "" && enumTable.Data[i][1] != "")
                    {
                        enumBuilder.DefineLiteral(enumTable.Data[i][1], j);
                        i++;
                        j++;

                        if (i + 1 >= enumTable.Data.Count || enumTable.Data[i + 1][0] != "")
                        {
                            enumBuilder.DefineLiteral(enumTable.Data[i][1], j);
                            break;
                        }
                    }

                    if (enumTable.Data[i][0] != "")
                    {
                        i--;
                    }

                    EnumTypes.Add(enumName, enumBuilder.CreateType());
                }
            }
        }

        /// <summary>
        /// 校验CSV文件
        /// </summary>
        private static void VerifyCSV()
        {
            foreach (var csvFile in csvFiles)
            {
                if (!ValidateKeyData(csvFile, out int position)) {
                    Console.WriteLine(csvFile.FileName + $"的主键有重复的，请检查，位置为{position}");
                    throw new InvalidDataException("有重复的主键");
                }
            }

            bool ValidateKeyData(CSVFile csvFile,out int position)
            {
                if (!csvFile.HasKey) {
                    // 如果没有键列，则直接返回true
                    position = -2;
                    return true;
                }

                HashSet<string> uniqueKeys = new HashSet<string>();

                for (int i = 5; i < csvFile.Data.Count; i++) {
                    List<string> rowData = csvFile.Data[i];
                    string key = string.Empty;

                    if (csvFile.SingleKey) {
                        // 如果只有一个键列，则直接取对应列的数据作为键值
                        key = rowData[csvFile.Key];
                    }
                    else {
                        // 如果有多个键列，则将所有键列的数据拼接成一个字符串作为键值
                        foreach (int keyIndex in csvFile.Keys) {
                            key += rowData[keyIndex];
                        }
                    }

                    if (uniqueKeys.Contains(key)) {
                        // 如果存在重复的键值，则校验失败
                        position = i;
                        return false;
                    }

                    uniqueKeys.Add(key);
                }

                if (csvFile.Children.Count > 0)
                {
                    //开始校验子表
                    foreach (var child in csvFile.Children)
                    {
                        for (int i = 5; i < child.Data.Count; i++) {
                            List<string> rowData = child.Data[i];
                            string key = string.Empty;

                            if (child.SingleKey) {
                                // 如果只有一个键列，则直接取对应列的数据作为键值
                                key = rowData[child.Key];
                            }
                            else {
                                // 如果有多个键列，则将所有键列的数据拼接成一个字符串作为键值
                                foreach (int keyIndex in child.Keys) {
                                    key += rowData[keyIndex];
                                }
                            }

                            if (uniqueKeys.Contains(key)) {
                                // 如果存在重复的键值，则校验失败
                                position = i;
                                return false;
                            }

                            uniqueKeys.Add(key);
                        }
                    }
                }

                // 所有键值都是唯一的，校验通过
                position = -1;
                return true;
            }
        }

        private static void GenerateDataManagerScriptFile()
        {
            //CSV的主键代表了唯一性，单个主键的CSV文件可以使用Dictionary来管理，多个的得需要多键字典，不过现在应该只有单键的表，等用到了多键在添加
            var root = SyntaxFactory.CompilationUnit();

            //需要用到的Using语句
            var usingDeclarations = new UsingDirectiveSyntax[]
            {
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Collections.Generic")),
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.IO")),
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Xml.Serialization")),
            };

            //得放在一个命名空间下
            var namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName("ConfigType")).NormalizeWhitespace();


            //DataManager类声明
            var classDeclaration = SyntaxFactory.ClassDeclaration("DataManager")
                .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                    SyntaxFactory.Token(SyntaxKind.PartialKeyword)))
                .WithBaseList(SyntaxFactory.BaseList(SyntaxFactory.SingletonSeparatedList<BaseTypeSyntax>(
                    SyntaxFactory.SimpleBaseType(SyntaxFactory.GenericName(SyntaxFactory.Identifier("Singleton"))
                        .WithTypeArgumentList(SyntaxFactory.TypeArgumentList(
                            SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
                                SyntaxFactory.IdentifierName("DataManager"))))))));

            //每个csv文件都要生成一套
            foreach (var csvFile in csvFiles) {
                var listType = SyntaxFactory.GenericName(SyntaxFactory.Identifier("List"))
                    .WithTypeArgumentList(SyntaxFactory.TypeArgumentList(SyntaxFactory.SingletonSeparatedList<TypeSyntax>(SyntaxFactory.IdentifierName(NameToDefine(csvFile.FileName)))));

                //最基础的List字段,初始化时需要将XML的数据反序列化出来赋值给List
                var listField = SyntaxFactory.FieldDeclaration(
                    SyntaxFactory.VariableDeclaration(listType)
                        .WithVariables(SyntaxFactory.SingletonSeparatedList(SyntaxFactory.VariableDeclarator($"{NameToDefine(csvFile.FileName)}List")
                            .WithInitializer(SyntaxFactory.EqualsValueClause(SyntaxFactory.ObjectCreationExpression(SyntaxFactory.GenericName(SyntaxFactory.Identifier("List")).WithTypeArgumentList(SyntaxFactory.TypeArgumentList(SyntaxFactory.SeparatedList<TypeSyntax>(new SyntaxNodeOrToken[]
                            {
                                SyntaxFactory.IdentifierName(NameToDefine(csvFile.FileName))
                            })))).WithArgumentList(SyntaxFactory.ArgumentList())))//初始化声明
                        )))//字段声明
                    .NormalizeWhitespace()
                    .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)));


                //降低复杂度的字典,也可以不要然后通过遍历List来查，这个就是用空间换时间了
                var dictionaryField = SyntaxFactory.FieldDeclaration(
                    SyntaxFactory.VariableDeclaration(
                            SyntaxFactory.GenericName(
                                    SyntaxFactory.Identifier("Dictionary"))
                        .WithTypeArgumentList(
                            SyntaxFactory.TypeArgumentList(
                                SyntaxFactory.SeparatedList<TypeSyntax>(
                                    new SyntaxNodeOrToken[]{
                                        SyntaxFactory.IdentifierName(GetTypeSyntax(csvFile.Data[1][csvFile.Key]).ToString()),
                                        SyntaxFactory.Token(SyntaxKind.CommaToken),
                                        SyntaxFactory.IdentifierName(NameToDefine(csvFile.FileName))}))))
                    .WithVariables(
                        SyntaxFactory.SingletonSeparatedList<VariableDeclaratorSyntax>(
                            SyntaxFactory.VariableDeclarator(
                                    SyntaxFactory.Identifier($"{NameToDefine(csvFile.FileName)}Dic"))
                            .WithInitializer(
                                SyntaxFactory.EqualsValueClause(
                                    SyntaxFactory.ObjectCreationExpression(
                                            SyntaxFactory.GenericName(
                                                    SyntaxFactory.Identifier("Dictionary"))
                                        .WithTypeArgumentList(
                                            SyntaxFactory.TypeArgumentList(
                                                SyntaxFactory.SeparatedList<TypeSyntax>(
                                                    new SyntaxNodeOrToken[]{
                                                        SyntaxFactory.IdentifierName(GetTypeSyntax(csvFile.Data[1][csvFile.Key]).ToString()),
                                                        SyntaxFactory.Token(SyntaxKind.CommaToken),
                                                        SyntaxFactory.IdentifierName(NameToDefine(csvFile.FileName))}))))
                                    .WithArgumentList(
                                        SyntaxFactory.ArgumentList()))))))
                    .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)));


                //通过主键获得数据的方法
                var methodDeclaration = SyntaxFactory.MethodDeclaration(
                        SyntaxFactory.IdentifierName(NameToDefine(csvFile.FileName)),
                    $"Get{NameToDefine(csvFile.FileName)}By{csvFile.Data[2][csvFile.Key]}")
                    .WithParameterList(
                        SyntaxFactory.ParameterList(
                            SyntaxFactory.SingletonSeparatedList(
                                SyntaxFactory.Parameter(SyntaxFactory.Identifier(csvFile.Data[2][csvFile.Key]))
                                .WithType(SyntaxFactory.IdentifierName(GetTypeSyntax(csvFile.Data[1][csvFile.Key]).ToString())))))
                    .WithBody(
                        SyntaxFactory.Block(
                            SyntaxFactory.SingletonList<StatementSyntax>(
                                SyntaxFactory.ReturnStatement(
                                    SyntaxFactory.ElementAccessExpression(
                                        SyntaxFactory.IdentifierName($"{NameToDefine(csvFile.FileName)}Dic"))
                                    .WithArgumentList(
                                        SyntaxFactory.BracketedArgumentList(
                                            SyntaxFactory.SingletonSeparatedList(
                                                SyntaxFactory.Argument(SyntaxFactory.IdentifierName(csvFile.Data[2][csvFile.Key])))))))))
                    .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)));

                classDeclaration = classDeclaration.AddMembers(listField,dictionaryField,methodDeclaration);
            }

            //将XML文件反序列化成数据并且赋值给对应的字段方法
            var serializerMethodDeclaration = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("void"), "InitConfigs")
                .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                .WithBody(SyntaxFactory.Block(GenerateSerializerCode()));

            //初始化辅助字典方法
            var initializeDictionaryMethodDeclaration = SyntaxFactory
                .MethodDeclaration(SyntaxFactory.ParseTypeName("void"), "InitDictionary")
                .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                .WithBody(SyntaxFactory.Block(GenerateInitDictionaryCode()));


            classDeclaration = classDeclaration.AddMembers(serializerMethodDeclaration);


            classDeclaration = classDeclaration.AddMembers(initializeDictionaryMethodDeclaration);



            namespaceDeclaration = namespaceDeclaration.AddMembers(classDeclaration);

            root = root.AddUsings(usingDeclarations);

            root = root.AddMembers(namespaceDeclaration);

            var code = root.NormalizeWhitespace().ToFullString();

            if (!Directory.Exists(_dataManagerScriptDir))
            {
                Directory.CreateDirectory(_dataManagerScriptDir);
            }


            File.WriteAllText(_dataManagerScriptDir + "DataManager.cs", code);
        }

        private static List<StatementSyntax> GenerateInitDictionaryCode()
        {
            var statements = new List<StatementSyntax>();

            foreach (var csvFile in csvFiles)
            {
                var foreachStatement = SyntaxFactory.ForEachStatement(SyntaxFactory.Token(SyntaxKind.ForEachKeyword),
                    SyntaxFactory.Token(SyntaxKind.OpenParenToken), SyntaxFactory.ParseTypeName("var"),
                    SyntaxFactory.Identifier("i"), SyntaxFactory.Token(SyntaxKind.InKeyword),
                    SyntaxFactory.ParseExpression($"{NameToDefine(csvFile.FileName)}List"),
                    SyntaxFactory.Token(SyntaxKind.CloseParenToken), SyntaxFactory.Block(
                        new List<StatementSyntax>()
                        {
                            SyntaxFactory.ParseStatement(
                                $"{NameToDefine(csvFile.FileName)}Dic.Add(i.{csvFile.Data[2][csvFile.Key]},i);")
                        }
                    ));

                statements.Add(foreachStatement);
            }

            return statements;
        }

        private static List<StatementSyntax> GenerateSerializerCode() {
            var statements = new List<StatementSyntax>
            {
                SyntaxFactory.ParseStatement("string ConfigPath = \"Assets/Resources/Config/xml/\";")
            };

            foreach (var file in csvFiles)
            {
                var variableName = file.FileName;

                var fileStreamLine = SyntaxFactory.ParseStatement($"FileStream {variableName}Stream = File.OpenRead(ConfigPath + \"{file.FileName}.xml\");");
                var serializerLine = SyntaxFactory.ParseStatement($"XmlSerializer {NameToDefine(variableName)}serializer = new XmlSerializer(typeof(List<{NameToDefine(variableName)}>));");
                var deserializeLine = SyntaxFactory.ParseStatement($"{NameToDefine(variableName)}List = (List<{NameToDefine(variableName)}>){NameToDefine(variableName)}serializer.Deserialize({variableName}Stream);");

                statements.Add(fileStreamLine);
                statements.Add(serializerLine);
                statements.Add(deserializeLine);
            }

            statements.Add(SyntaxFactory.ParseStatement("InitDictionary();"));

            return statements;
        }

        private static void CopyToGameProject()
        {
            //先删除游戏工程中的文件
            if (Directory.Exists(_gameXMLDir))
            {
                Directory.Delete(_gameXMLDir,true);
            }

            if (File.Exists(_gameConfigTypeDir + "ConfigType.cs"))
            {
                File.Delete(_gameConfigTypeDir + "ConfigType.cs");
            }

            //拷贝文件
            Directory.CreateDirectory(_gameXMLDir);
            foreach (var file in Directory.GetFiles(_outputXMLDir))
            {
                File.Copy(file, _gameXMLDir + Path.GetFileName(file));
            }

            Directory.CreateDirectory(_gameConfigTypeDir);
            File.Copy(_configTypeCSDir,_gameConfigTypeDir + "ConfigType.cs");
        }

        static void GenerateConfigTypeScriptFile()
        {
            //CSV表的第二行为数据类型
            //CSV表的第三行为字段名
            //CSV表的第四行为注释
            //CSV表的第五行为若未填写数据时，使用的默认值
            //CSV表的第六行开始为数据

            var namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName("ConfigType"));

            var generatorType = CSharpSyntaxTree.ParseText(File.ReadAllText("DataManager\\DataManager\\GeneratorType.cs"));

            foreach (var syntaxNode in generatorType.GetRoot().ChildNodes())
            {
                if (syntaxNode is ClassDeclarationSyntax classSyntax)
                {
                    Console.WriteLine("Config加入一个GeneratorType中的Class");
                    namespaceDeclaration = namespaceDeclaration.AddMembers(classSyntax);
                }
            }

            //所有的枚举类型声明
            foreach (var enumKVP in EnumTypes) {
                EnumDeclarationSyntax enumDeclaration = SyntaxFactory.EnumDeclaration(enumKVP.Key).WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)));
                // 获取枚举类型的所有字段
                var fields = enumKVP.Value.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);


                // 添加枚举值
                foreach (var field in fields) {
                    var identifier = SyntaxFactory.Identifier(field.Name);
                    var enumMember = SyntaxFactory.EnumMemberDeclaration(identifier);
                    enumDeclaration = enumDeclaration.AddMembers(enumMember);
                }

                namespaceDeclaration = namespaceDeclaration.AddMembers(enumDeclaration);
            }

            //所有的类声明
            foreach (CSVFile csvFile in csvFiles)
            {
                string className = csvFile.FileName;


                // 解析CSV文件
                List<string> types = csvFile.Data[1];
                List<string> names = csvFile.Data[2];
                List<string> comments = csvFile.Data[3];

                // 生成类的属性
                List<PropertyDeclarationSyntax> properties = new List<PropertyDeclarationSyntax>();
                for (int i = 0; i < types.Count; i++) {
                    if (!string.IsNullOrEmpty(types[i])) {
                        string type = types[i];
                        string name = names[i];
                        string comment = comments[i];

                        // 添加注释
                        SyntaxTriviaList triviaList = SyntaxFactory.ParseTrailingTrivia($"// {comment} \n");

                        // 添加属性
                        PropertyDeclarationSyntax property = SyntaxFactory
                            .PropertyDeclaration(GetTypeSyntax(type), name)
                            .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                            .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                            .WithTrailingTrivia(triviaList);

                        properties.Add(property);
                    }
                }

                // 生成类
                ClassDeclarationSyntax classDecl = SyntaxFactory.ClassDeclaration(className + "Define")
                    .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword),SyntaxFactory.Token(SyntaxKind.PartialKeyword)))
                    .WithMembers(SyntaxFactory.List<MemberDeclarationSyntax>(properties))
                    .WithTrailingTrivia(SyntaxFactory.CarriageReturnLineFeed);

                namespaceDeclaration = namespaceDeclaration.AddMembers(classDecl);
            }


            // 生成输出文件
            CompilationUnitSyntax output = SyntaxFactory.CompilationUnit()
                .WithUsings(SyntaxFactory.List(new[]
                {
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Collections.Generic")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Linq"))
                }))
                .WithMembers(SyntaxFactory.SingletonList<MemberDeclarationSyntax>(namespaceDeclaration))
                .WithTrailingTrivia(SyntaxFactory.CarriageReturnLineFeed);

            string outputFile = Path.Combine(_outputDir, "ConfigType.cs");
            using (var writer = new StreamWriter(outputFile)) {
                output.NormalizeWhitespace().WriteTo(writer);
            }

            Console.WriteLine($"已生成输出文件：{outputFile}");
        }

        static void GenerateData() {
            //通过Roslyn加载ConfigType.cs的所有的类，生成对应类的数值
            var configType = File.ReadAllText(_configTypeCSDir);
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(configType);
            CSharpCompilation compilation = CSharpCompilation.Create("MyCompilation")
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                .AddReferences(MetadataReference.CreateFromFile(typeof(System.Linq.EnumerableQuery).Assembly.Location))
                .AddSyntaxTrees(syntaxTree);
            using (var ms = new MemoryStream()) {
                var result = compilation.Emit(ms);
                if (!result.Success) {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                                               diagnostic.IsWarningAsError ||
                                                                      diagnostic.Severity == DiagnosticSeverity.Error);
                    foreach (Diagnostic diagnostic in failures) {
                        Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    }
                    return;
                }

                ms.Seek(0, SeekOrigin.Begin);

                Assembly assembly = Assembly.Load(ms.ToArray());

                foreach (CSVFile csvFile in csvFiles) {

                    //读取CSV文件，然后再在ConfigType.cs中找到对应的类，然后生成对应的数据
                    string className = csvFile.FileName;

                    Type classType = assembly.GetType("ConfigType." + NameToDefine(className));


                    if (classType != null)
                    {
                        var listType = typeof(List<>).MakeGenericType(classType);
                        var listObj = Activator.CreateInstance(listType);
                        for (int i = 5; i < csvFile.Data.Count; i++) {
                            //每一行都代表一个实例
                            object instance = Activator.CreateInstance(classType);
                            var typeName = csvFile.PropertyName;
                            for (int j = 0; j < typeName.Count; j++) {
                                FieldInfo property = classType.GetField(typeName[j]);
                                if (!string.IsNullOrEmpty(csvFile.Data[i][j]))
                                {
                                    //有填值，用填的值
                                    property.SetValue(instance, ConverPropertyType(csvFile.Data[i][j], property.FieldType));
      
                                }
                                else
                                {
                                    //没填值，尝试用默认值
                                    if (!string.IsNullOrEmpty(csvFile.Data[4][j])) {
                                        //使用默认值
                                        property.SetValue(instance, ConverPropertyType(csvFile.Data[4][j], property.FieldType));
                                    }
                                    else
                                    {
                                        if (property.FieldType == typeof(string))
                                        {
                                            property.SetValue(instance, string.Empty);
                                        }
                                        else
                                        {
                                            object defaultValue = Activator.CreateInstance(property.FieldType);
                                            property.SetValue(instance, defaultValue);
                                        }

                                    }
                                }

                            }

                            MethodInfo addMethod = listType.GetMethod("Add");
                            addMethod.Invoke(listObj,  new object?[]{ instance });
       
                        }

                        //主表生成完了，尝试生成子表
                        if (csvFile.Children.Count > 0)
                        {
                            Console.WriteLine("有子表，开始额外添加子表的数据");
                            foreach (var child in csvFile.Children)
                            {
                                //规则:主表有的，子表不一定有，子表没有的就用主表的默认值
                                for (int i = 5; i < child.Data.Count; i++) {
                                    //每一行都代表一个实例
                                    object instance = Activator.CreateInstance(classType);
                                    var typeName = child.Parent.PropertyName;
                                    for (int j = 0; j < typeName.Count; j++) {
                                        FieldInfo property = classType.GetField(typeName[j]);
                                        if (child.GetValueByPropertyName(typeName[j],i,out string propertyStr) && !string.IsNullOrEmpty(propertyStr))
                                        {
                                            //有填值，用填的值
                                            property.SetValue(instance, ConverPropertyType(propertyStr, property.FieldType));
                                        }
                                        else
                                        {
                                            //TODO:子表没有这个属性或者没填值，先尝试用子表的默认值，如果也没有，尝试用父表默认值
                                            if (child.GetValueByPropertyName(typeName[j], 4, out string childDefaultValue) && !string.IsNullOrEmpty(childDefaultValue)){
                                                //使用默认值
                                                property.SetValue(instance, ConverPropertyType(childDefaultValue, property.FieldType));
                                            }
                                            else {
                                                //TODO:尝试用父表的默认值
                                                if (!string.IsNullOrEmpty(csvFile.Data[4][j]))
                                                {
                                                    property.SetValue(instance, ConverPropertyType(csvFile.Data[4][j], property.FieldType));
                                                }
                                                else
                                                {
                                                    //都没有，用默认值
                                                    if (property.FieldType == typeof(string)) {
                                                        property.SetValue(instance, string.Empty);
                                                    }
                                                    else {
                                                        object defaultValue = Activator.CreateInstance(property.FieldType);
                                                        property.SetValue(instance, defaultValue);
                                                    }
                                                }


                                            }
                                        }
                                    }

                                    MethodInfo addMethod = listType.GetMethod("Add");
                                    addMethod.Invoke(listObj, new object?[] { instance });

                                }
                            }
                        }

                        //序列化后写入文件
                        XmlSerializer serializer = new XmlSerializer(listType, new Type[] { classType });

                        using (MemoryStream xmlMs = new MemoryStream()) {
                            using (XmlWriter writer = XmlWriter.Create(xmlMs, new XmlWriterSettings() { Indent = true, IndentChars = "\t" })) {
                                serializer.Serialize(writer, listObj);
                                if (!Directory.Exists(_outputXMLDir)) {
                                    Directory.CreateDirectory(_outputXMLDir);
                                }

                                File.WriteAllBytes(_outputXMLDir + $"//{className}.xml", xmlMs.ToArray());
                            }
                        }

                    }
                    else {
                        Console.WriteLine("没有在ConfigType.cs中找到对应的类型数据");
                    }

                }

            }

            Console.WriteLine("已将生成了对应的XML数据");
        }

        public static object ConverPropertyType(string value, Type type)
        {
            try
            {
                if (type == typeof(int)) {
                    return int.Parse(value);
                }
                else if (type == typeof(float)) {
                    return float.Parse(value);
                }
                else if (type == typeof(bool)) {
                    return bool.Parse(value);
                }
                else if (type == typeof(string)) {
                    return value;
                }
                else if (type.IsEnum) {
                    if (value == "") {
                        Console.WriteLine("枚举值是空的，默认返回0");
                        return Enum.Parse(type, "0");
                    }
                    return Enum.Parse(type, value);
                }else if (type.Name == "EditableType")
                {
                    var editType = Activator.CreateInstance(type);
                    var field = type.GetField("TypeName");
                    field.SetValue(editType,value);
                    return editType;
                }
                else if (type.GetGenericTypeDefinition() == typeof(List<>)) {
                    //列表类型
                    var listObj = Activator.CreateInstance(type);
                    //分割value的值，然后填入列表
                    var values = value.Split(';');
                    foreach (var item in values) {
                        if (string.IsNullOrEmpty(item))
                        {
                            continue;
                        }
                        var itemValue = ConverPropertyType(item, type.GetGenericArguments()[0]);
                        MethodInfo addMethod = type.GetMethod("Add");
                        addMethod.Invoke(listObj, new object?[] { itemValue });
                    }


                    return listObj;
                }
                else {
                    throw new Exception("不支持的类型");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"将字符串转换为对应数据类型时出现错误，类型为:{type},值为:{value}");
                Console.WriteLine("===========报错堆栈=========");
                Console.WriteLine(e);
                Console.WriteLine("============================");
                throw;
            }

        }

        static TypeSyntax GetTypeSyntax(string type) {
            switch (type.ToLower()) {
                case "string":
                    return SyntaxFactory.ParseTypeName("string");
                case "int32":
                    return SyntaxFactory.ParseTypeName("int");
                case "float":
                    return SyntaxFactory.ParseTypeName("float");
                case "bool":
                    return SyntaxFactory.ParseTypeName("bool");
                case "int32[]":
                    return SyntaxFactory.ParseTypeName("List<int>");
                case "float[]":
                    return SyntaxFactory.ParseTypeName("List<float>");
                case "bool[]":
                    return SyntaxFactory.ParseTypeName("List<bool>");
                case "string[]":
                    return SyntaxFactory.ParseTypeName("List<string>");
                case "type":
                    return SyntaxFactory.ParseTypeName("ConfigType.EditableType");
                default:
                    //枚举类型或者枚举类型数组
                    Console.WriteLine($"可能时枚举类型或者枚举数组，type = {type},EnumTypesCount = {EnumTypes.Count}");
                    if (type.EndsWith("[]"))
                    {
                        var enumType = type.Substring(0, type.Length - 2);
                        if (EnumTypes.ContainsKey(enumType))
                        {
                            //枚举类型数组
                            return SyntaxFactory.ParseTypeName($"List<{enumType}>");
                        } 
                    }
                    else if (EnumTypes.ContainsKey(type))
                    {
                        return SyntaxFactory.ParseTypeName(type);
                    }
                    throw new ArgumentException($"Unknown type: {type}");
                    
            }
        }

        private static string NameToDefine(string name)
        {
            return name + "Define";
        }
    }

    public class CSVFile
    {
        public CSVFile Parent;

        public string FileName;

        public List<List<string>> Data = new List<List<string>>();

        public List<int> Keys;

        /// <summary>
        /// 备注:子表暂时分一层就够了，不会继续往下找
        /// </summary>
        public List<CSVFile> Children = new List<CSVFile>();
        /// <summary>
        /// 大部分表都是单个主键，所以弄个方法方便点
        /// </summary>
        public int Key => Keys[0];

        public List<string> PropertyName => Data[2];

        public List<string> DefaultValue => Data[4];

        public bool HasKey => Keys.Count > 0;

        public bool SingleKey => Keys.Count == 1;

        public string GetData(int row, int col)
        {
            return Data[row][col];
        }

        public List<string> GetRow(int sheet, int row)
        {
            return Data[row];
        }



        public bool GetValueByPropertyName(string propertyTypeName,int dataIndex, out string value)
        {
            for (int i = 0; i < PropertyName.Count; i++)
            {
                if (PropertyName[i] == propertyTypeName)
                {
                    value = Data[dataIndex][i];
                    return true;
                }
            }

            value = null;
            return false;
        }

        public static CSVFile ReadCSVFile(string path)
        {
            //TODO:遇到同名同类型的列,需要合并到第一次出现的地方
            CSVFile csvFile = new CSVFile();

            csvFile.FileName = Path.GetFileNameWithoutExtension(path);
            csvFile.Data = new List<List<string>>();

            var dataArray = File.ReadAllLines(path,Encoding.GetEncoding("GB2312"));

            foreach (var data in dataArray) {            
                //WPS会把中文变为GB2312，很烦,把WPS的妈妈偷走，这边手动转一下UTF8
                byte[] gb2312Bytes = Encoding.GetEncoding("GB2312").GetBytes(data);
                byte[] utf8Bytes = Encoding.Convert(Encoding.GetEncoding("GB2312"), Encoding.UTF8, gb2312Bytes);

                var splitData = Encoding.UTF8.GetString(utf8Bytes).Split(',');
                
                csvFile.Data.Add(splitData.ToList());
            }

            csvFile.SetKeys();

            CombineSameNameRow(csvFile);

            return csvFile;
        }

        public static void CombineSameNameRow(CSVFile csvFile)
        {
            var name = csvFile.PropertyName;
            Dictionary<string, int> nameToIndex = new Dictionary<string, int>();
            List<int> colToRemove = new List<int>();

            for (int i = 0; i < name.Count; i++) {
                string columnName = name[i];
                if (nameToIndex.ContainsKey(columnName)) {
                    int firstIndex = nameToIndex[columnName];
                    for (int j = 5; j < csvFile.Data.Count; j++) {
                        csvFile.Data[j][firstIndex] += ";" + csvFile.Data[j][i];
                    }
                    colToRemove.Add(i);
                }
                else {
                    nameToIndex[columnName] = i;
                }
            }

            // 删除重复的列
            for (int i = colToRemove.Count - 1; i >= 0; i--) {
                int columnIndex = colToRemove[i];
                foreach (var row in csvFile.Data) {
                    row.RemoveAt(columnIndex);
                }
            }

            if (csvFile.Children is { Count: > 0 } )
            {
                for (int i = 0; i < csvFile.Children.Count; i++)
                {
                    CombineSameNameRow(csvFile.Children[i]);
                }
            }

        }

        private void SetKeys()
        {
            //找到Data中第一行为*号的列
            Keys = new List<int>();
            for (int i = 0; i < Data[0].Count; i++)
            {
                if (Data[0][i].StartsWith("*"))
                {
                    Keys.Add(i);
                }
            }
        }

    }
}