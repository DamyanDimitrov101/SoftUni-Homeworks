const sumFun = require('../index.js').sum;
const chai = require('chai');
const { it } = require('mocha');

describe('Sum function',function () {
    it('Should receive array as arguement', ()=>{
        let num = 4;

        chai.assert.throws(()=> sumFun(num), TypeError);
    });

    it('Should return positive number when adding two positive numbers', ()=>{
        let first = 2;
        let second = 5;

        let arr = [first,second];

        let res =  sumFun(arr);

        chai.assert.equal(res,7);
    });

    it('Should return 0 when the array is empty',()=>{
        let arr = [];

        let res = sumFun(arr);

        chai.assert.equal(res,0);
    });
})