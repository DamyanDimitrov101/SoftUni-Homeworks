const assert = require('chai').assert;
const rgbToHexColor = require('../index.js').rgbToHexColor;

describe('rgbToHexColor function',function () {
    it('Should return undefined if any of the input parameters are of invalid type',()=>{
        let res = rgbToHexColor(5,'sdds',54);

        assert.equal(res, undefined);
    });

    it('Should return undefined if any of the input parameters are not in the expected range',()=>{
        let res = rgbToHexColor(2,256,4);
        let res2 = rgbToHexColor(-1,5,3);

        assert.equal(res,undefined);
        assert.equal(res2,undefined);
    });

    it('Should return the same color in hexadecimal format as a string',()=> {
        let expected = '#FF9EAA';
        let res = rgbToHexColor(255, 158, 170);

        assert.equal(res,expected);
    });
})
