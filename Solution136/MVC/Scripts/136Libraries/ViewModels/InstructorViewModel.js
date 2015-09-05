function InstructorViewModel() {

    var InstructorModelObj = new InstructorModel();
    var self = this;
    var initialBind = true;
    var instructorListViewModel = ko.observableArray();

    this.Initialize = function() {

        var viewModel = {
            id: ko.observable(),
            first: ko.observable("Nick"),
            last: ko.observable("Test"),
            title: ko.observable("Teacher"),
            password: ko.observable("password"),
            add: function (data) {
                self.CreateInstructor(data);
            }
        };

        ko.applyBindings(viewModel, document.getElementById("divInstructor"));
    };

    this.CreateInstructor = function (data) {
        var model = {
            InstructorId: data.id(),
            FirstName: data.first(),
            LastName: data.last(),
            Title: data.title(),
            Password: data.password()
        }

        InstructorModelObj.Create(model, function (result) {
            if (result == "ok") {
                alert("Created Instructor successfully. Login email is: " + model.FirstName.toLowerCase().substring(0, 1) + model.LastName.toLowerCase() + "@ucsd.edu");
            } else {
                alert("Error creating Instructor occurred");
            }
        });

    };

    this.GetAll = function() {

        InstructorModelObj.GetAll(function (instructorList) {
            instructorListViewModel.removeAll();

            for (var i = 0; i < instructorList.length; i++) {
                instructorListViewModel.push({
                    id: instructorList[i].InstructorId,
                    first: instructorList[i].FirstName,
                    last: instructorList[i].LastName,
                    title: instructorList[i].Title,
                    password: instructorList[i].Password
                });
            }

            if (initialBind) {
                ko.applyBindings({ viewModel: instructorListViewModel }, document.getElementById("divInstructorListContent"));
                initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
            }
        });
    };

    this.GetDetail = function (id) {

        InstructorModelObj.GetDetail(id, function (result) {
            
            var instructor = {
                id: ko.observable(result.InstructorId),
                first: ko.observable(result.FirstName),
                last: ko.observable(result.LastName),
                title: ko.observable(result.Title),
                password: ko.observable(result.Password),
                update: function () {
                    self.UpdateInstructor(this);
                }
            };

            if (initialBind) {
                ko.applyBindings(instructor , document.getElementById("divInstructorContent"));
            }
        });
    };

    this.UpdateInstructor = function (instructor) {
        var InstructorModelObj = new InstructorModel();

        // convert the viewModel to same structure as PLAdmin model (presentation layer model)
        var instructorData = {
            InstructorId: instructor.id,
            FirstName: instructor.first(),
            LastName: instructor.last(),
            Title: instructor.title(),
            Password: instructor.password()
        };

        InstructorModelObj.Update(instructorData, function (message) {
            $('#divMessage').html(message);
        });

    };

    ko.bindingHandlers.DeleteInstructor = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).click(function () {
                var id = viewModel.id;

                InstructorModelObj.Delete(id, function (result) {
                    if (result != "ok") {
                        alert("Error Deleting Instructor Occurred");
                    } else {
                        instructorListViewModel.remove(viewModel);
                    }
                });
            });
        }
    }
}
