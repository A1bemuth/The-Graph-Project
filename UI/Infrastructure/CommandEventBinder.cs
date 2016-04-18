namespace UI.Infrastructure
{
    public static class CommandEventBinder
    {
        public static Command LoadGraphCommand = new Command();
        public static Command CloseMenuCommand = new Command();
        public static Command ShowCyclesCommand = new Command();
        public static Command SelectCycleCommand = new Command();
        public static Command ShowPathCommand = new Command();
        public static Command SelectPathCommand = new Command();
        public static Command RefreshCommand = new Command();
    }
}