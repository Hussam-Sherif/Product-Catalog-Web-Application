namespace Product_Catalog_Web_Application.Core.Const
{
    public class Errors
    {
        public const string RequiredField = "Required field";
        public const string MaxLength = "{1} can not be more than 100 Charachter";
        public const string MaxMinLength = "The {0} must be at least {2} and at max {1} characters long.";
        public const string Duplicate = "Another record with the same {0} is already exists! ";
        public const string DuplicateBook = "You can not add this {0} as this is already exist with the same Author! ";
        public const string OnlyEnglishLetters = "Only English letters are allowed.";
    }
}
