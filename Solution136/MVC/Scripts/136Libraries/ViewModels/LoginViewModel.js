function LoginViewModel() {
    var self = this;

    var logonViewModel = {
        email: ko.observable('admin@cs.ucsd.edu'),
        password: ko.observable('password'),
        authorize: function () {
            self.Authorize(this.email(), this.password());
        }
    };

    this.Initialize = function () {
        ko.applyBindings(logonViewModel, document.getElementById("divLogin"));
    };

    this.Authorize = function (email, password) {

        var loginModelObj = new LoginModel();

        // Because the Load() is a async call (asynchronous), we'll need to use
        // the callback approach to handle the data after data is loaded.
        loginModelObj.AuthenticateGet(email, password, function (logon) {
            alert('Result using get:' + logon.Role);
        });

        loginModelObj.AuthenticatePost(email, password, function (logon) {
            alert('Result using post:' + logon.Role);
        });
    };
}