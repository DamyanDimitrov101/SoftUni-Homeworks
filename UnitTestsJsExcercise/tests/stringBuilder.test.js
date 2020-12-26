let {assert} = require('chai');
let StringBuilder = require('../stringBuilder.js');

describe('StringBuilder class',function () {
    let sb;
    beforeEach(()=> {
        sb = new StringBuilder();
    })

    describe('constructor',function () {
        it('Should return empty string with no arguments',()=>{
            let current = new StringBuilder();

            assert.equal('',current.toString());
        });

        it('Should work properly',()=>{
            sb = new StringBuilder('redo');

            assert.equal('redo',sb.toString());
        });
    })

    describe('_vrfyParam function',function () {
        it('Should throw typeError with other than string passed as argument',()=>{
            assert.throw(()=> {new StringBuilder(5)},'Argument must be string');
            assert.throw(()=> {new StringBuilder(true)},'Argument must be string');
        });
    })

    describe('append function',function () {
        it('Should push string input in the end of the internal array',()=>{
            sb.append('kra')
            sb.append('ka')
            sb.append('a')
            assert.equal('krakaa', sb,toString());
        });
    })

    describe('prepend function',function () {
        it('Should push string input in the beginning of the internal array',()=>{
            sb.prepend('kra')
            sb.prepend('ka')
            sb.prepend('a')
            assert.equal('akakra', sb,toString());
        });
    })

    describe('insertAt function',function () {
        it('Should return undefined if startIndex does not exist as position of the internal array',()=>{
            assert.equal(undefined, sb.insertAt('ka',-1));            
            assert.equal(undefined, sb.insertAt('kra',99));
        });

        it('Should push string input in the exact position of the internal array',()=>{
            sb.insertAt('kra',0)
            sb.insertAt('ka',1)
            assert.equal('kkara', sb,toString());
        });
    })

    describe('remove function',function () {
        it('Should not remove string from the internal array if startIndex is negative',()=>{
            sb.append('kra')
            sb.append('ka')
            sb.append('a')
            

            assert.equal(undefined, sb.remove(0,-3));
        });

        it('Should remove string from the internal array if startIndex is bigger than length of the array',()=>{
            sb.append('kra')
            sb.append('ka')
            sb.append('a')
            
            sb.remove(0,33)

            assert.equal('', sb.toString());
        });

        it('Should remove string from the internal array',()=>{
            sb.append('kra')
            sb.append('ka')
            sb.append('a')
            
            sb.remove(0,3)

            assert.equal('kaa', sb.toString());
        });
    })

    describe('toString function',function () {
        it('Should return the value of the internal array joined',()=>{
            sb.prepend('kra')
            sb.prepend('ka')
            sb.prepend('a')
            assert.equal('akakra', sb,toString());
        });
    })
})