using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VueBlog.API.Models;
using VueBlog.API.Services.Essay;
using VueBlog.API.Utility;

namespace VueBlog.API.Controllers
{
    [ApiController]
    [Route("api/essay")]
    public class EssayController:ControllerBase
    {
        private readonly IEssayService essayService;
        public EssayController(IEssayService essayService)
        {
            this.essayService = essayService;
        }


        [HttpPost("addessay")]
        [Authorize]
        public async Task<ActionResult<Models.Essay>> AddEssayAsync(Models.Essay essay)
        {
            try
            {
                var res = await essayService.CreateEssayAsync(essay);
                return Ok(res);
            }
            catch(Exception)
            {
                return StatusCode(500, "服务器内部错误");
            }

        }
        [HttpGet]
        public async Task<ActionResult<List<Models.EssayDTO>>> GetAllEssaysAsync([FromQuery]int page)
        {
            try
            {
                var res = await essayService.GetEssaysAsync(page);
                if(res == null)
                {
                    return NoContent();
                }
                return Ok(EssayDTOConverter.Converter(res));
            }
            catch
            {
                return StatusCode(500, "服务器内部错误");
            }
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<EssayDTO>> DeleteEssayAsync(Guid id)
        {
            try
            {
                Essay res = await essayService.DeleteEssayAsync(id);
                return Ok(EssayDTOConverter.Converter(res));
            }
            catch (Exception)
            {
                return StatusCode(500, "服务器内部错误");
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<EssayDTO>> UpdateEssayAsync(Guid id,Essay essay)
        {
            try
            {
                Essay res = await essayService.UpdateEssayAsync(id, essay);
                return Ok(EssayDTOConverter.Converter(res));
            }
            catch (Exception)
            {
                return StatusCode(500, "服务器内部错误");
            }
        }
        [HttpGet("tag")]
        public async Task<ActionResult<List<EssayDTO>>> GetEssaysByTagsAsync([FromQuery] string[] tags,[FromQuery] int page)
        {
            try
            {
                List<Essay> res = await essayService.GetEssaysByTagAsync(tags,page);
                if(res == null)
                {
                    return NoContent();
                }
                return Ok(EssayDTOConverter.Converter(res));
            }
            catch (Exception)
            {
                return StatusCode(500, "服务器内部错误");
            }
        } 
    }
}
