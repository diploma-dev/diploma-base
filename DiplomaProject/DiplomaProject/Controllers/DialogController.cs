using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API.Completions;
using OpenAI_API;
using DiplomaProject.Secrets;

namespace DiplomaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "BaseUser")]
    public class DialogController : ApiBaseController
    {
        [HttpPost("message")]
        public async Task<IActionResult> WriteMessage([FromBody] string prompt)
        {
            var openAIClient = new OpenAIAPI(AppSecret.OpenAISettings.OpenAIKey);

            CompletionRequest completion = new CompletionRequest()
            {
                Prompt = prompt,
                Model = OpenAI_API.Models.Model.DavinciText,
                MaxTokens = 3500
            };

            var response =  await openAIClient.Completions.CreateCompletionAsync(completion);

            if (response != null)
            {
                string answer = string.Empty;

                response.Completions.ForEach(x => answer = x.Text);

                return Ok(answer);
            }

            return Ok(Messages.OpenAIError);
        }
    }
}
