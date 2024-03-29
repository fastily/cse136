﻿namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using IRepository;
    using POCO;

    public class CapeReviewRepository : BaseRepository, ICapeReviewRepository
    {
        private cse136Entities context;

        public CapeReviewRepository(cse136Entities entities)
        {
            this.context = entities;
        }

        public void InsertCape(CapeReview cr, ref List<string> errors)
        {
            cape_reviews db_CapeReview = new cape_reviews();

            try
            {
                db_CapeReview.cape_id = cr.CapeId;
                ////db.instructor_id = this.context.instructors.Where(x => x.last_name == cr.InstructorName).Select(y => y.instructor_id).First();

                db_CapeReview.instructor_id = cr.InstructorId;
                db_CapeReview.course_id = cr.CourseId;
                db_CapeReview.instructor_rating = cr.InstructorRating;
                db_CapeReview.summary = cr.Summary;
                db_CapeReview.course_rating = cr.CourseRating;

                this.context.cape_reviews.Add(db_CapeReview);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in CapeReviewRepository.InsertCape: " + e);
            }
        }

        public void DeleteCapeReview(int cape_id, ref List<string> errors)
        {
            cape_reviews db_CapeReview = new cape_reviews();
   
            try
            {
                db_CapeReview.cape_id = cape_id;
                this.context.cape_reviews.Remove(db_CapeReview);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in CapeReviewRepository.DeleteCapeReview: " + e);
            }
        }

        public List<CapeReview> FindCapeReviewsByCourseId(int course_id, ref List<string> errors)
        {
            List<CapeReview> pocoCrList = new List<CapeReview>();

            try
            {
                IEnumerable<cape_reviews> db_crList = this.context.cape_reviews.Where(x => x.course_id == course_id);

                foreach (cape_reviews db in db_crList)
                {
                    var pocoCr = new CapeReview();
                    pocoCr.CapeId = db.cape_id;
                    pocoCr.CourseId = (int)db.course_id;
                    pocoCr.InstructorId = (int)db.instructor_id;
                    pocoCr.InstructorRating = (int)db.instructor_rating;
                    pocoCr.Summary = db.summary;
                    pocoCr.CourseRating = (int)db.course_rating;
                    pocoCrList.Add(pocoCr);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in CapeReviewRepository.FindCapeReviewById: " + e);
            }

            return pocoCrList;
        }

        public List<CapeReview> FindCapeReviewsByInstructorId(int instructor_id, ref List<string> errors)
        {
            List<CapeReview> pocoCrList = new List<CapeReview>();

            try
            {
                IEnumerable<cape_reviews> db_crList = this.context.cape_reviews.Where(x => x.instructor_id == instructor_id);

                foreach (cape_reviews db in db_crList)
                {
                    var pocoCr = new CapeReview();
                    pocoCr.CapeId = db.cape_id;
                    pocoCr.CourseId = (int)db.course_id;
                    pocoCr.InstructorId = (int)db.instructor_id;
                    pocoCr.InstructorRating = (int)db.instructor_rating;
                    pocoCr.Summary = db.summary;
                    pocoCr.CourseRating = (int)db.course_rating;
                    pocoCrList.Add(pocoCr);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in CapeReviewRepository.FindCapeReviewById: " + e);
            }

            return pocoCrList;
        }

        public CapeReview FindCapeReviewByScheduleId(int schedule_id, ref List<string> errors)
        {
            CapeReview pocoCR = new CapeReview();
            course_schedule db_schedule = new course_schedule();
            cape_reviews db_cr = new cape_reviews();

            try
            {
                db_schedule = this.context.course_schedule.Find(schedule_id);
                db_cr = this.context.cape_reviews.Where(x => x.course_id == db_schedule.course_id &&
                                x.instructor_id == (int)db_schedule.instructor_id).First();
                pocoCR.CapeId = db_cr.cape_id;
                pocoCR.CourseId = (int)db_cr.course_id;
                pocoCR.CourseRating = (int)db_cr.course_rating;
                pocoCR.InstructorId = (int)db_cr.instructor_id;
                pocoCR.InstructorRating = (int)db_cr.instructor_rating;
                pocoCR.Summary = db_cr.summary;
            }
            catch (Exception e)
            {
                errors.Add("Error occured in CapeReviewRepository.FindCapeReviewById: " + e);
            }

            return pocoCR;
        }

        public void UpdateCapeReview(CapeReview cr, ref List<string> errors)
        {
            cape_reviews db = new cape_reviews();

            try
            {
                db.cape_id = cr.CapeId;
                db.instructor_id = cr.InstructorId;
                db.course_id = cr.CourseId;
                db.course_rating = cr.CourseRating;
                db.instructor_rating = cr.InstructorRating;
                db.summary = cr.Summary;

                this.context.cape_reviews.Add(db);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in CapeReviewRepository.UpdateCapeReview: " + e);
            }
        }
    }
}
