function LoginModel() {

    // "this" object in Javascript is not the same as C# "this" keyword.
    // In JavaScript, "this" object is the object that is current executing the method
    // so "this" object changes as program calls different methods.
    // The best way to retain the "this" pointer is to assign to another variable.
    // Common name to use it "self" or "that".
    var self = this;

    this.AuthenticateGet = function (email, password, callback) {

        // This is demo code only.  In reality, you don't want to use "GET" to pass user/password
        // info because it's clearly visible in the url
        $.ajax({
            method: 'GET',
            url: "http://localhost:9393/Api/Authorize/Authenticate?email=" + email + "&password=" + password,
            data: "",
            dataType: "json",
            success: function (logon) {
                callback(logon); 
            },
            error: function () {
                // if the call fails, it will return FirstName="First" and LastName="Last"
                alert('Error while calling authenticate method using get method.');http://localhost:36638/Scripts/136Libraries/ViewModels
                callBack(self);
            }
        });
    };

    this.AuthenticatePost = function (email, password, callback) {
        var data = { UserName: email, Password: password };

        // This is the correct way to pass user/password info because it's a POST.
        // In reality, you would use https and all header info & post data would be encrypted
        // except for the url itself.
        $.ajax({
            method: 'POST',
            url: "http://localhost:9393/Api/Authorize/Authenticate",
            data: data, 
            success: function (logon) {
                callback(logon);
            },
            error: function () {
                callback('Error while posting to authenticate method');
            }
        });
    };
}
