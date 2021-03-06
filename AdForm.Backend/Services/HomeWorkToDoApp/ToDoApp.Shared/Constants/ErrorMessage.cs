namespace ToDoApp.Shared
{
    public static  class ErrorMessage
    {
        public static readonly string User_Invalid_Login = "Invalid username or password";
        public static readonly string User_UserName_Empty = "Please enter your username";
        public static readonly string User_Password_Empty = "Please enter your password";

        public static readonly string Item_Exist = "This item is allready exists";
        public static readonly string Item_Not_Exist = "This item is not exists";
        public static readonly string Item_Name_Empty = "Please provied item name";
        public static readonly string Item_Id_Null = "Please provied item Id";

        public static readonly string List_Exist = "This todolist is allready exists";
        public static readonly string List_Not_Exist = "This todolist is not exists";
        public static readonly string List_Name_Empty = "Please provied ttodolis name";
        public static readonly string List_Id_Null = "Please provied list Id";

        public static readonly string Label_Exist = "This label is allready exists";
        public static readonly string Label_Not_Exist = "This label is not exists";
        public static readonly string Label_Name_Empty = "Please provied label name";
        public static readonly string Label_Not_Exist_assign = "This label is not exists and can not assign any todolist or todoitem";
    }
}
