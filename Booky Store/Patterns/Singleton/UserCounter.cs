namespace Booky_Store.Patterns.Singleton
{
    public class UserCounter
    {
        public int count = 0;
        private static UserCounter instance = new UserCounter();

        private UserCounter() { }

        public static UserCounter GetInstance()
        {
            return instance;
        }
        public void AddOne()
        {
            count++;
        }
    }
}
