﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Exceptions
{
    public abstract class ServiceException : ArgumentException
    {
        public ServiceException(int errorCode, string errorMessage) : base(errorMessage)
        {
            this.ErrorCode = errorCode;
        }
        public int ErrorCode { get; set; }
        public string Field { get; set; }
    }
}
