namespace UI.Infrastructure
{
    public static class CommandEventBinder
    {
        public static Command LoadGraphCommand = new Command();
        public static Command LoadingComplited = new Command();

        public static Command SelectCycleCommand = new Command();
        public static Command SelectPathCommand = new Command();
        public static Command RefreshCommand = new Command();

        public static Command OpenMenuCommand = new Command();
        public static Command CloseMenuCommand = new Command();
        public static Command ShowCyclesModalCommand = new Command();
        public static Command CloseCyclesModalCommand = new Command();
        public static Command ShowPathModalCommand = new Command();
        public static Command ClosePathModalCommand = new Command();

        public static Command CathedExcaptionCommand = new Command();
        public static Command AllCycleFound = new Command();
    }
}