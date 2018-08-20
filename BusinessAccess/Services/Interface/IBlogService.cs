using BusinessAccess.DataContract;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAccess.Services.Interface
{
    public interface IBlogService
    {
        List<BlogContract> GetAllBlogs();
    }
}
