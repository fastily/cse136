namespace Service
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using POCO;

    public class CapeReviewService
    {
        private readonly ICapeReviewRepository repository;

        public CapeReviewService(ICapeReviewRepository repository)
        {
            this.repository = repository;
        }

        public int GetCourseRating(int course_id, ref List<string> errors)
        {
            var courseRating = 0;
            List<CapeReview> capeReviewList = new List<CapeReview>();
            var reviewCount = 0;

            if (string.IsNullOrEmpty(course_id.ToString()))
            {
                errors.Add("Invalid course id");
                throw new ArgumentException();
            }

            capeReviewList = this.repository.FindCapeReviewsByCourseId(course_id, ref errors);
            foreach (CapeReview cr in capeReviewList)
            {
                if (cr.CourseRating > 0)
                {
                    reviewCount++;
                    courseRating += cr.CourseRating;
                }
            }

            return courseRating / reviewCount;
        }

        public int GetInstructorRating(int instructor_id, ref List<string> errors)
        {
            var instructorRating = 0;
            List<CapeReview> capeReviewList = new List<CapeReview>();
            var reviewCount = 0;

            if (string.IsNullOrEmpty(instructor_id.ToString()))
            {
                errors.Add("Invalid course id");
                throw new ArgumentException();
            }

            capeReviewList = this.repository.FindCapeReviewsByInstructorId(instructor_id, ref errors);
            foreach (CapeReview cr in capeReviewList)
            {
                if (cr.InstructorRating > 0)
                {
                    reviewCount++;
                    instructorRating += cr.InstructorRating;
                }
            }

            return instructorRating / reviewCount;
        }

        ////find cape reviews by scheduleId

        ////insert cape review schedule id, student id, instructor id
    }
}
