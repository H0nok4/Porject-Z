using System;

public class CmdRegAttribute : Attribute
{
    public string[] CmdArray;

    public CmdRegAttribute(params string[] cmds)
    {
        CmdArray = cmds;
    }
}