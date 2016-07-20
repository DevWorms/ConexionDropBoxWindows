using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanda.AppTray
{
    public static class RegExpConstants
    {
        public const string REGEXP_RFC = "^[a-zA-Z]{3,4}([0-9]{6}[0-9a-zA-Z]{3}|[0-9]{6})$";
        public const string REGEXP_EMAIL = "^[a-zA-Z0-9_\\+-]+(\\.[a-zA-Z0-9_\\+-]+)*@[a-zA-Z0-9-]+(\\.[a-zA-Z0-9-]+)*\\.([a-zA-Z]{2,4})$";
        public const string REGEXP_ALPHANUMEXT = "^[0-9a-zA-Z_ \\-á-úÁ-Úä-üÄ-Ü''\\.:,\\(\\)\\/\\#\\\\*\\$\"!@%&=?¡¿+-;]*$";
        public const string REGEXP_ALPHANUMEXTHTML = "^[0-9a-zA-Z_ \\-á-úÁ-Úä-üÄ-Ü''\\.:,<>!\\(\\)\\/\\#]*$";
        public const string REGEXP_ISNUMERIC = "^[0-9]*$";
        public const string REGEXP_ISPHONEA = @"^([0-9]+-?)*[0-9]$";
        public const string REGEXP_ZIPCODE_FIVEDIGITS = "^[0-9]{5}$";
        public const string REGEXP_ALPHANUMEXTCOMMNETS = "^[0-9a-zA-Z_ \\-á-úÁ-Úä-üÄ-Ü''\\.,\\(\\)\\/\\n\\r]*$";
        public const string REGEXP_DATE = "^[0-9]{2}[/-][0-9]{2}[/-][0-9]{2}$";
        public const string REGEXP_ISDECIMAL = "^[0-9].*$";
        public const string REGEXP_ISDECIMAL_P = "(?:-)?(?:0|[1-9]\\d{0,3})?(?:\\.[0-9]{0,2})?";
        public const string REGEXP_LOGIN = "^(?=.*\\d)(?=.*[A-Z]).{7,20}$";

    }
}
