using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOATester.Infrastructure.Enums {
    public enum Protocol {
        [Description("default")]
        DEFAULT,
        [Description("http")]
        HTTP,
        [Description("https")]
        HTTPS
    }

    public enum Method {
        [Description("get")]
        GET,
        [Description("post")]
        POST,
        [Description("put")]
        PUT,
        [Description("patch")]
        PATCH,
        [Description("delete")]
        DELETE,
        [Description("head")]
        HEAD
    }
}
