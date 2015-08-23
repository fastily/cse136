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

        public void InsertCapeReview(CapeReview cr, ref List<string> errors)
        {
            if (cr == null)
            {
                errors.Add("Cape Review cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(cr.InstructorId.ToString()))
            {
                errors.Add("Instructor cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(cr.CourseId.ToString()))
            {
                errors.Add("Course cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(cr.Summary))
            {
                errors.Add("Summary cannot be null");
                throw new ArgumentException();
            }

            this.repository.InsertCape(cr, ref errors);
        }

        public void DeleteCapeReview(int cape_id, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(cape_id.ToString()))
            {
                errors.Add("Cape Review Id cannot be null");
                throw new ArgumentException();
            }

            this.repository.DeleteCapeReview(cape_id, ref errors);
        }

        public void UpdateCapeReview(CapeReview cr, ref List<string> errors)
        {
            if (cr == null)
            {
                errors.Add("Cape Review cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(cr.InstructorId.ToString()))
            {
                errors.Add("Instructor cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(cr.CourseId.ToString()))
            {
                errors.Add("Course cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(cr.Summary))
            {
                errors.Add("Summary cannot be null");
                throw new ArgumentException();
            }

            this.repository.UpdateCapeReview(cr, ref errors);
        }
    }
}
