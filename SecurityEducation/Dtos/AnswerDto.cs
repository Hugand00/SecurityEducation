﻿namespace SecurityEducation.Dtos
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCorrect { get; set; }        
        public int QuestionId { get; set; }
    }
}
