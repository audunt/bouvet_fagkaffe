using bouvet_fagkaffe_repository;
using bouvet_fagkaffe_repository.Models;

namespace bouvet_fagkaffe_frontend.Services
{
    public class LectureHelper(Operations operations)
    {
        public Operations _operations = operations;

        public async Task<Lecture> ConvertToLecture(Candidate candidate, List<User> presenters)
        {
            Lecture lecture = new()
            {
                Title = "",
                Topic = candidate.Topic,
                Summary = candidate.Summary,
                Department = candidate.Department,
            };
            lecture.HeldBy.AddRange(presenters);
            return await _operations.CreateLectureFromCandidate(lecture, candidate.Id);
        }
    }
}
