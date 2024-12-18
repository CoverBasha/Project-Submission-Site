﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Mail_Verification_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGmailVerify" in both code and config file together.
    [ServiceContract]
    public interface IGmailVerify
    {
        [OperationContract]
        string SendMail(string email,string username);

        [OperationContract]
        bool SendModificationNotify(string email, string username, string project, string desc);
    }
}
