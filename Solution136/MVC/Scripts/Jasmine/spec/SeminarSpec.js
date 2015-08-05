var SeminarFactory = {
    create: function (overwrites) {
        var defaults = { name: "JavaScript-Basics", price: 500, taxFree: false };
        var values = Object.extend(defaults, overwrites);
        return Seminar.create(values.name, values.price, values.taxFree);
    }
};

Object.extend = function (obj, props) {
    for (var prop in props) {
        obj[prop] = props[prop];
    }
    return obj;
};

describe("Seminar", function () {
    var seminar;

    it("should have a name", function () {
        seminar = SeminarFactory.create({ name: "JavaScript-Basics" });
        expect(seminar.name()).toEqual('JavaScript-Basics');
    });

    it("should have a price", function () {
        seminar = SeminarFactory.create({ price: 499.99 });
        expect(seminar.netPrice()).toEqual(499.99);
    });

    it("should have a gross_price that adds 20% VAT to the net price", function () {
        seminar = SeminarFactory.create({ price: 100 });
        expect(seminar.grossPrice()).toEqual(120);
    });
});

describe("A tax-free Seminar", function () {
    var seminar;

    beforeEach(function () {
        seminar = SeminarFactory.create({ taxFree: true });
    });

    it("should be tax-free", function () {
        expect(seminar).toBeTaxFree(true);
    });

    it("should have a gross_price that matches the net price", function () {
        expect(seminar.grossPrice()).toEqual(seminar.netPrice());
    });
});

describe("A 3-letter Seminar", function() {
    var seminar;

    beforeEach(function() {
        seminar = SeminarFactory.create({ name: "BDD" });
    });

    it("should have a 3letter-discount granted", function() {
        expect(seminar).toHave3LetterDiscountGranted(true);
    });

    it("should give a 5% discount", function () {
        expect(seminar.discountPercentage()).toEqual(5);
    });

    describe("that is priced $200", function () {

        beforeEach(function () {
            seminar = SeminarFactory.create({ name: "BDD", price: 200 });
        });

        it("should have a discount of $10", function () {
            expect(seminar.discount()).toEqual(10);
        });

        it("should have a net price of $190", function () {
            expect(seminar.netPrice()).toEqual(190);
        });
    });
});

describe("A non 3-letter Seminar", function () {
    var seminar;

    beforeEach(function () {
        seminar = SeminarFactory.create({ name: "BDD123" });
    });

    it("should not have a 3letter-discount granted", function () {
        expect(seminar).toHave3LetterDiscountGranted(false);
    });

    it("should give a 0% discount", function () {
        expect(seminar.discountPercentage()).toEqual(0);
    });
});

