var student = {
    StudentId: "test1234",
    SSN: "555991234",
    FirstName: "TestFirst",
    LastName: "TestLast",
    Email: "Test@Test.com",
    Password: "Password",
    ShoeSize: 10,
    Weight: 200
};

var studentModelObj = new StudentModel(false);

describe("Student Object", function () {

    it("can create a new student", function () {
        studentModelObj.Create(student, function (result) {
            expect(result).toEqual('ok');
        });
    });

    it("can return a student detail info by id", function () {
        studentModelObj.GetDetail(student.StudentId, function (detailResult) {
            expect(detailResult.StudentId).toEqual(student.StudentId);
            expect(detailResult.SSN).toEqual(student.SSN);
            expect(detailResult.FirstName).toEqual(student.FirstName);
            expect(detailResult.LastName).toEqual(student.LastName);
            expect(detailResult.Email).toEqual(student.Email);
            expect(detailResult.Password).toEqual(student.Password);
            expect(detailResult.ShoeSize).toEqual(student.ShoeSize);
            expect(detailResult.Weight).toEqual(student.Weight);
        });
    });

    it("can delete an existing student by id", function () {
        studentModelObj.Delete(student.StudentId, function (result) {
            expect(result).toEqual('ok');
        });
    });


    it("can return a list of students in the database", function () {
        studentModelObj.GetAll(function (result) {
            expect(result.length).toBeGreaterThan(0);
        });
    });
});
