let { lookupChar } = require('../charLookup.js');
let { assert } = require('chai');

describe('lookupChar function', function () {
    it('Should return undefined if other than string type is passed as argument - string', () => {
        assert.equal(undefined,lookupChar(true,0));
        assert.equal(undefined,lookupChar({},0));
        assert.equal(undefined,lookupChar(6,0));
    });

    it('Should return undefined if other than Number type is passed as argument - index', () => {
        assert.equal(undefined,lookupChar('true',{}));
        assert.equal(undefined,lookupChar('dsd','ds'));
        assert.equal(undefined,lookupChar('dsd',5.5));
    });

    it('Should return undefined if Number is out of range of the string input as argument - index', () => {
        assert.equal("Incorrect index",lookupChar('true',4));
        assert.equal("Incorrect index",lookupChar('dsd',-4));
    });

    it('Should work properly and return the exact char',()=>{
        assert.equal('a',lookupChar('wassup',1));
    });
})