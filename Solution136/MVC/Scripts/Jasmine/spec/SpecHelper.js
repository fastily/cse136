beforeEach(function () {{}
    jasmine.addMatchers({        
        toBeTaxFree: function () {
            return {
                compare: function (actual, expected) {
                    var seminar = actual;

                    return {
                        pass: seminar.isTaxFree() == expected
                    }
                }
            };
        },

        toHave3LetterDiscountGranted: function () {
            return {
                compare: function(actual, expected) {
                    var seminar = actual;
                    var result = { pass: seminar.have3letterDiscountGranted() == expected };

                    if (result.pass) {
                        result.message = "Success! '" + seminar.name() + "' received discount";
                    } else {
                        result.message = "Error! '" + seminar.name() + "' should not be receiving discount";
                    }

                    return result;
                }
            };
        }
	});
});
