using System.Text.Json.Serialization;

namespace VueBlog.API.Models
{
    public class EssayDTO
    {

        public Guid Id { get; set; } = Guid.NewGuid(); // 数据库唯一 ID

        public string Slug { get; set; } = string.Empty; // 文章唯一标识

        public int Views { get; set; } = 0; // 浏览量

        public int Likes { get; set; } = 0; // 点赞数

        public int CommentsCount { get; set; } = 0; // 评论数

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // 创建时间

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // 更新时间

        public string Excerpt { get; set; } = string.Empty; // 摘要

        public string Title { get; set; } = string.Empty; // 标题
        public string Content { get; set; } = string.Empty; // 内容
        public string Author { get; set; } = string.Empty; // 作者
        public string? CoverImage { get; set; } // 封面图片，可选
        public List<string> Tags { get; set; } = new(); // 标签列表
        public string Category { get; set; } = string.Empty; // 分类
        public bool IsPublished { get; set; } = false; // 是否发布
        public string? MetaDescription { get; set; } // SEO 描述，可选
    }
}
