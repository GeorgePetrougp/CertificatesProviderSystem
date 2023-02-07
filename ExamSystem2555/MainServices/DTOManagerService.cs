using WebApp.DTO_Models;

namespace WebApp.MainServices
{
    public class DTOManagerService
    {
        public DTOManagerService()
        {
            
        }

        public CreateQuestionView InitializeQuestionView()
        {
            var questionView = new CreateQuestionView();


            return questionView;
        }
    }
}
