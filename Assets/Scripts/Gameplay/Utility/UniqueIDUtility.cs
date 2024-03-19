public static class UniqueIDUtility {
    private static int _uid = 1;

    public static int GetUID() {
        return _uid++;
    }
}