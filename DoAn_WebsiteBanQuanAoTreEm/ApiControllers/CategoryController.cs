using DoAn_WebsiteBanQuanAoTreEm.Filters;
using DoAn_WebsiteBanQuanAoTreEm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DoAn_WebsiteBanQuanAoTreEm.ApiControllers
{
    [AdminAuthorization]
    public class CategoryController : ApiController
    {
        private MyDbContext db = new MyDbContext();

        public class CategoryDto
        {
            public int MaLoai { get; set; }
            public string TenLoai { get; set; }
        }

        public List<CategoryDto> Get()
        {
            var categories = db.loaiQuanAos
                .Select(lq => new CategoryDto
                {
                    MaLoai = lq.maLoai,
                    TenLoai = lq.tenLoai
                })
                .ToList();

            return categories;
        }

        public IHttpActionResult GetCategoryByID(int id)
        {
            LoaiQuanAo loai = db.loaiQuanAos.Find(id);
            if (loai == null)
            {
                return NotFound();
            }

            return Ok(new CategoryDto
            {
                MaLoai = loai.maLoai,
                TenLoai = loai.tenLoai
            });
        }

        [HttpPost]
        public IHttpActionResult CreateCategory([FromBody] LoaiQuanAo category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.loaiQuanAos.Add(category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category.maLoai }, category);
        }

        [HttpPut]
        public IHttpActionResult UpdateCategory(int id, [FromBody] LoaiQuanAo category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.maLoai)
            {
                return BadRequest();
            }

            db.Entry(category).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        public IHttpActionResult DeleteCategory(int id)
        {
            LoaiQuanAo category = db.loaiQuanAos.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            db.loaiQuanAos.Remove(category);
            db.SaveChanges();

            return Ok(category);
        }

        private bool CategoryExists(int id)
        {
            return db.loaiQuanAos.Count(e => e.maLoai == id) > 0;
        }
    }
}
