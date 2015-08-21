namespace Repository
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
        /// <summary>
        /// This class's context
        /// </summary>
        private cse136Entities context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entities">The entity to use</param>
        public CapeReviewRepository(cse136Entities entities)
        {
            this.context = entities;
        }

        /// <summary>
        /// Inserts a cape review into the db
        /// </summary>
        /// <param name="cr">The poco cape review to insert</param>
        /// <param name="errors">Error list</param>
        public void InsertCape(CapeReview cr, ref List<string> errors)
        {
            cape_reviews db = new cape_reviews();

            try
            {
                db.cape_id = cr.CapeId;
                ////db.instructor_id = this.context.instructors.Where(x => x.last_name == cr.InstructorName).Select(y => y.instructor_id).First();

                db.instructor_id = cr.InstructorId;
                db.course_id = cr.CourseId;
                db.instructor_rating = cr.InstructorRating;
                db.summary = cr.Summary;
                db.course_rating = cr.CourseRating;

                this.context.cape_reviews.Add(db);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in CapeReviewRepository.InsertCape: " + e);
            }
        }

        /// <summary>
        /// Deletes a cape review from the db
        /// </summary>
        /// <param name="cape_id">ID of the cape review to delete</param>
        /// <param name="errors">Errors List</param>
        public void DeleteCapeReview(int cape_id, ref List<string> errors)
        {
            cape_reviews db = new cape_reviews();
   
            try
            {
                db.cape_id = cape_id;
                this.context.cape_reviews.Remove(db);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in CapeReviewRepository.DeleteCapeReview: " + e);
            }
        }

        /// <summary>
        /// 
        /// Finds a cape review by Id in the db
        /// </summary>
        /// <param name="cape_id">id to search for</param>
        /// <param name="errors">Error list</param>
        /// <returns></returns>
        public CapeReview FindCapeReviewByCourseId(int course_id, ref List<string> errors)
        {
            CapeReview pocoCR = new CapeReview();

            try
            {
                cape_reviews db = this.context.cape_reviews.Find(course_id);

                pocoCR.CapeId = db.cape_id;
                pocoCR.CourseId = (int)db.course_id;
                pocoCR.InstructorId = (int)db.instructor_id;
                pocoCR.InstructorRating = (int)db.instructor_rating;
                pocoCR.Summary = db.summary;
                pocoCR.CourseRating = (int)db.course_rating;
            }
            catch (Exception e)
            {
                errors.Add("Error occured in CapeReviewRepository.FindCapeReviewById: " + e);
            }

            return pocoCR;
        }

        /// <summary>
        /// Updates a cape review in db
        /// </summary>
        /// <param name="cr">cape review to update (will create new entry if specified doesn't exist), seems to only update dirty fields</param>
        /// <param name="errors">error list</param>
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
