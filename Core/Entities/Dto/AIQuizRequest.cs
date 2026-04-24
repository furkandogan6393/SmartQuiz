using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Dto
{
    public class AIQuizRequest : IDto
    {
        public List<int> DocumentIds { get; set; }
        public string Prompt { get; set; }
        public int QuestionCount { get; set; }
        public int OptionCount { get; set; }
        public string DifficultyTemplate { get; set; }
    }
}
