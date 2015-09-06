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

        public float GetCourseRating(int course_id, ref List<string> errors)
        {
            var courseRating = 0;
            List<CapeReview> capeReviewList = new List<CapeReview>();
            var reviewCount = 0;

            //// can never fail default to == 0 for all service layers
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

        public float GetInstructorRating(int instructor_id, ref List<string> errors)
        {
            var instructorRating = 0;
            List<CapeReview> capeReviewList = new List<CapeReview>();
            var reviewCount = 0;

            if (instructor_id <= 0)
            {
                errors.Add("Invalid course id");
                throw new NullReferenceException();
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

            if (cr.InstructorId <= 0)
            {
                errors.Add("Instructor cannot be null");
                throw new ArgumentException();
            }

            if (cr.CourseId <= 0)
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
            if (cape_id <= 0)
            {
                errors.Add("Cape Review Id cannot be null");
                throw new NullReferenceException();
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

            if (cr.InstructorId <= 0)
            {
                errors.Add("Instructor cannot be null");
                throw new ArgumentException();
            }

            if (cr.CourseId <= 0)
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

        public List<CapeReview> GetCapeReview(int cid, ref List<string> errors)
        {
            return this.repository.FindCapeReviewsByCourseId(cid, ref errors);
        }
    }
}
