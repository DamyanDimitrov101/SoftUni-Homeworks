const assert = require('chai').assert;
const createCalculator = require('../index.js').createCalculator;

describe('createCalculator function',function () {
    it('Should return a module (object), containing the functions add(), subtract() and get() as properties',()=>{
        let res = createCalculator();

        assert.property(res,'add');
        assert.property(res,'subtract');
        assert.property(res,'get');
    });

    it('Should keep an internal sum which can’t be modified from the outside',()=>{
        let res = createCalculator();
        let expected = 0;

        assert.equal(res.get(),expected)
    });

    it('Should add() and subtract() take a parameter that can be parsed as a number (either a number or a string containing a number) that is added or subtracted from the internal sum',()=>{
        let number = 44;
        let stringContainingANum = '42';

        let res = createCalculator();

        res.add(number);
        assert.equal(res.get(),44);

        res.add(stringContainingANum);
        assert.equal(res.get(),86);

        res.subtract(stringContainingANum);
        assert.equal(res.get(),44);

        res.subtract(number);
        assert.equal(res.get(),0);
    });

    it('get() should return the value of the internal sum',()=>{
        let res = createCalculator();
        res.add(-43);

        assert.equal(res.get(),-43);
        res.add(43);

        res.subtract(-1);
        assert.equal(res.get(),1);
    });

    it('Should return undefined if string with not a number passed',()=>{
        let res = createCalculator();

        assert.equal(res.add('shit'),undefined)        
    });
})
