let {assert} = require('chai');
let {isOddOrEven} = require('../evenOrOdd.js');

describe('isOddOrEven function',function () {
    it('Should return undefined when other than strign arguement is given',()=>{
        assert.equal(undefined, isOddOrEven(3));
    });

    it('Should return odd when strign arguement with odd length is given',()=>{
        assert.equal('odd', isOddOrEven('sde'));
    });

    it('Should return even when strign arguement with even length is given',()=>{
        assert.equal('even', isOddOrEven('edse'));
    });

    it('Should return correct values with multiple checks',()=>{
        assert.equal('even', isOddOrEven('edse'));
        assert.equal('odd', isOddOrEven('sde'));
        assert.equal('odd', isOddOrEven('dsasd'));
        assert.equal('even', isOddOrEven('even'));
        
    });
})
