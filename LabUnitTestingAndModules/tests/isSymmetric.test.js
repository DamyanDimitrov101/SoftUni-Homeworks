const assert = require('chai').assert;
const isSymmetric = require('../index.js').isSymmetric;

describe('isSymmetric function', function () {
    it('Should take an array as arguement otherwise returns false',()=>{
        let res = isSymmetric(5);

        assert.equal(res,false);
    });

    it('Should be symmetric',()=>{
        let array = [5,6,6,5];

        let res = isSymmetric(array);

        assert.equal(res,true);
    });

    it('Should be symmetric and if not returns false',()=>{
        let array = [5,6,6,2];

        let res = isSymmetric(array);

        assert.equal(res,false);
    });
})