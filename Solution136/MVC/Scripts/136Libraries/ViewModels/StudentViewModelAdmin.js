function StudentViewModelAdmin() {
    var viewModel = null;
    var StudentModelObj = new StudentModelAdmin();
    var self = this;
    var initialBind = true;
    var studentListViewModel = ko.observableArray();

    this.Initialize = function() {

        var viewModel = {
            id: ko.observable("A0000111"),
            first: ko.observable("Bruce"),
            last: ko.observable("Wayne"),
            email: ko.observable("bwayne@ucsd.edu"),
            password: ko.observable("password"),
            add: function (data) {
                self.CreateStudent(data);
            }
        };

        ko.applyBindings(viewModel, document.getElementById("divStudent"));
    };

    /* Check login3 for this example where viewModel is outside of function */
    this.Initialize2 = function () {
        ko.applyBindings(viewModel, document.getElementById("divStudentContent"));
    };

    this.CreateStudent = function(data) {
        var model = {
            StudentId: data.id(),
            FirstName: data.first(),
            LastName: data.last(),
            Email: data.email(),
            Password: data.password()
        }

        StudentModelObj.Create(model, function(result) {
            if (result == "ok") {
                alert("Created student successfully");
            } else {
                alert("Error occurred");
            }
        });

    };

    this.GetAll = function() {

        StudentModelObj.GetAll(function(studentList) {
            studentListViewModel.removeAll();

            for (var i = 0; i < studentList.length; i++) {
                studentListViewModel.push({
                    id: studentList[i].StudentId,
                    first: studentList[i].FirstName,
                    last: studentList[i].LastName,
                    email: studentList[i].Email
                });
            }

            if (initialBind) {
                ko.applyBindings({ viewModel: studentListViewModel }, document.getElementById("divStudentListContent"));
                initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
            }
        });
    };

    this.UpdateStudent = function (student) {
        var studentData = {
            StudentId: student.id(),
            FirstName: student.first(),
            LastName: student.last(),
            Email: student.email(),
            Password: student.password()
        };

        StudentModelObj.Update(studentData, function (message) {
            $('#divMessage').html(message);
        });

    };

    this.GetDetail = function (id) {

        StudentModelObj.GetDetail(id, function (result) {
            
            var student = {
                id: ko.observable(result.StudentId),
                first: ko.observable(result.FirstName),
                last: ko.observable(result.LastName),
                email: ko.observable(result.Email),
                password: ko.observable(result.Password),
                update: function () {
                    self.UpdateStudent(this);
                }
            };

            if (initialBind) {
                ko.applyBindings(student, document.getElementById("divStudentContent"));
            }
        });
    };

    ko.bindingHandlers.DeleteStudent = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).click(function () {
                var id = viewModel.id;

                StudentModelObj.Delete(id, function(result) {
                    if (result != "ok") {
                        alert("Error occurred");
                    } else {
                        studentListViewModel.remove(viewModel);
                    }
                });
            });
        }
    }
}
