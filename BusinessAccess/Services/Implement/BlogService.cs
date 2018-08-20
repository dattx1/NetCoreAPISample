using AutoMapper;
using BusinessAccess.DataContract;
using BusinessAccess.Repository;
using BusinessAccess.Services.Interface;
using DataAccess.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessAccess.Services.Implement
{
    public class BlogService : IBlogService
    {
        private readonly IRepository<Blog> _blogRepository;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        public BlogService(IRepository<Blog> blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _logger = LogManager.GetLogger(typeof(BlogService));
        }

        public List<BlogContract> GetAllBlogs()
        {
            var result = _blogRepository.GetAll().ToList();
            if (result.Count <= 0)
                return new List<BlogContract>();
            return _mapper.Map<List<Blog>, List<BlogContract>>(result);
        }
    }
}
