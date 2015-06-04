using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Profile;

namespace ModelCasc.webApp
{
    public class ProfileCasc: ProfileBase
    {
        public static ProfileCasc GetUserProfile(string username)
        {
            return (ProfileCasc)Create(username);
        }

        [SettingsAllowAnonymous(false)]
        public int Id_bodega
        {
            get
            {
                var o = base.GetPropertyValue("id_bodega");
                if (o == DBNull.Value)
                {
                    return 0;
                }
                return (Int32)o;
            }
            set
            {
                base.SetPropertyValue("id_bodega", value);
            }
        }

        [SettingsAllowAnonymous(false)]
        public string Nombre
        {
            get
            {
                var o = base.GetPropertyValue("nombre");
                if (o == DBNull.Value)
                {
                    return string.Empty;
                }
                return (string)o;
            }
            set
            {
                base.SetPropertyValue("nombre", value);
            }
        }
    }
}
