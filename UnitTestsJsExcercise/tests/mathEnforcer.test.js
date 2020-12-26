let {addFive,subtractTen,sum} = require('../mathEnforcer.js');
let {assert} = require('chai');

describe('mathEnforcer object',function () {
    describe('addFive function',function () {
        it('Should return undefined if type of input is other than Number',()=>{
            assert.equal(undefined,addFive('5'));
            assert.equal(undefined,addFive({}));
        });

        it('Should work as expected and add five to the current input value',()=>{
            assert.equal(10,addFive(5));
            assert.equal(1,addFive(-4));
        });
    })

    describe('subtractTen function',function () {
        it('Should return undefined if type of input is other than Number',()=>{
            assert.equal(undefined,subtractTen('5'));
            assert.equal(undefined,subtractTen({}));
        });

        it('Should work as expected and subtract ten from the current input value',()=>{
            assert.equal(-5,subtractTen(5));
            assert.equal(1,subtractTen(11));
        });
    })

    describe('sum function',function () {
        it('Should return undefined if type of inputs is other than Number',()=>{
            assert.equal(undefined,sum('5',5));
            assert.equal(undefined,sum(4,{}));
        });

        it('Should work as expected and sum  the current input values',()=>{
            assert.equal(5,sum(5,0));
            assert.equal(1,sum(-11,12));
        });
    })
})