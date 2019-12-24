namespace CourseProject
{
    class Checker
    {

        public Checker() { }

        public bool IsOnlyRussian(string input)
        {
            if (input.Length == 0)
            {
                return false;
            }

            input = input.ToLower();
            for (int i = 0; i < input.Length; i++)
            {
                if (!((input[i] >= 'а' && input[i] <= 'я') || (input[i] == 'ё')))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
