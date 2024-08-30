using OnlineStore.Common.Message;

namespace OnlineStore.Common.Message
{
    public class Messages
    {
        public const string RecordNotFound = "رکورد مورد نظر یافت نشد";

        #region Empty
        public const string InputIsEmpty = "ورودی خالی میباشد";
        public const string SomeInputsAreEmpty = "برخی از ورودی ها خالی میباشند";
        public const string InputIsNotValid = "ورودی معتبر نمیباشد";
        public const string IdIsEmpty = "شناسه خالی میباشد";

        public const string MobileNumberIsEmpty = "شماره موبایل خالی میباشد";
        public const string MobileNumberIsNotValid = "شماره موبایل معتبر نمیباشد";
        public const string UserMobileNumberIsNotValid = "شماره موبایل کاربر معتبر نمیباشد";
        #endregion

        #region Exception
        public const string CallApiFailed = "فراخوانی وب سرویس با خطا روبرو شد";
        public const string MessageBusPublishError = "ارسال به صف پیام با خطا روبرو شد";
        public const string ApiCallFailedFromOrganizationalStructure = "دریافت داده از سامانه ساختار سازمانی با خطا روبرو شد";
        public const string SendSmsFailed = "ارسال پیام کوتاه با خطا روبرو شد";
        public const string OperationFailed = "عملیات با خطا روبرو شد";
        #endregion

        #region Database
        public const string RecordHasReference = "رکورد، وابسته دارد، ابتدا وابسته ها میبایست حذف بشود";
        public const string ForeignKeyConstraint = "خطای عدم تطابق کلید خارجی در پایگاه داده رخ داده است";
        public const string UniqueIndex = "مقدار وارد شده تکراری می باشد";
        public const string DbUpdateConcurrencyException = "خطای همزمانی در پایگاه داده رخ داده است";
        #endregion

        #region Repetetive
        public const string FileAlreadyExist = "فایل از قبل وجود دارد";
        public const string RecordIsRepetetive = "رکورد تکراری میباشد";
        public const string TitleIsRepetetive = "عنوان تکراری میباشد";
        public const string FieldIsRepetetive = "{0} تکراری میباشد";
        public const string InputFilesAreRepetetive = "برخی از فایل های ورودی با هم مشابه هستند";
        #endregion

        #region Access
        public const string UserHasNoAccessToRecord = "کاربر به رکورد دسترسی ندارد";
        #endregion

        #region Other
        public const string EntityForAttachmentNotFound = "رکورد مربوط به پیوست یافت نشد";
        #endregion

        #region HttpStatusCodes
        public static MessageTemplate HttpCode_OK { get; set; } = new MessageTemplate("عملیات با موفقیت انجام شد", 200);
        public static MessageTemplate HttpCode_BadRequest { get; set; } = new MessageTemplate("خطای کاربر رخ داده است", 400);
        public static MessageTemplate HttpCode_Unauthorized { get; set; } = new MessageTemplate("کاربر احراز هویت نشده است", 401);
        public static MessageTemplate HttpCode_Forbidden { get; set; } = new MessageTemplate("کاربر دسترسی ندارد", 403);
        public static MessageTemplate HttpCode_NotFound { get; set; } = new MessageTemplate("محتوای مورد نظر یافت نشد", 404);
        public static MessageTemplate HttpCode_InternalServerError { get; set; } = new MessageTemplate("خطای سرور رخ داده است", 500);
        #endregion
    }
}
