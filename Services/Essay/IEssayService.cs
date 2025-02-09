namespace VueBlog.API.Services.Essay
{
    public interface IEssayService
    {
        //Get Essay(s)
        public Task<Models.Essay> GetEssayById(Guid id);
        public Task<List<Models.Essay>> GetEssaysAsync(int page);
        public Task<List<Models.Essay>> GetEssaysByTagAsync(string[] tags,int page);
        //Add Essay
        public Task<Models.Essay> CreateEssayAsync(Models.Essay essay);
        //Delete Essay
        public Task<Models.Essay> DeleteEssayAsync(Guid id);
        //Update Essay
        public Task<Models.Essay> UpdateEssayAsync(Guid id,Models.Essay essay);


    }
}
