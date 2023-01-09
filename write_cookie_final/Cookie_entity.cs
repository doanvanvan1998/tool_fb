using System;
using System.Collections.Generic;
using System.Text;

namespace write_cookie_final
{
    class Cookie_entity
    {
        private string path;
        private Dictionary<string,List<string>> list_value ;
        private List<string> password;

       

        public Cookie_entity()
        {

        }
        public void setPath(string path)
        {
            this.path = path;
        }
        public string getPath()
        {
            return this.path;
        }

        public void setListValue(Dictionary<string, List<string>> list_value)
        {
            this.list_value = list_value;
        }

        public Dictionary<string, List<string>> getListValue()
        {
            return this.list_value;
        }

        public void setPass(List<string> pass)
        {
            this.password = pass;
        }
        public List<string> getPass()
        {
            return this.password;
        }

    }
}
