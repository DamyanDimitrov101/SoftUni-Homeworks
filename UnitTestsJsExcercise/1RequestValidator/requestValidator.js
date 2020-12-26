function validate(obj) {
    let arrWithMethods = ['GET','POST','DELETE','CONNECT'];
    let uriRegexExp = /^([A-z0-9.]+)$|\*/gmi;
    let versionsArray = ['HTTP/0.9','HTTP/1.0','HTTP/1.1','HTTP/2.0'];
    let messageRegexExp = /^([^<>\\&"']+)$/gmi;

    if (!arrWithMethods.includes(obj.method) || !obj.hasOwnProperty('method')) {
        throw new Error('Invalid request header: Invalid Method');
    }

    if (!uriRegexExp.test(obj.uri) || !obj.hasOwnProperty('uri')) {
        throw new Error('Invalid request header: Invalid URI');
    }

    if (!versionsArray.includes(obj.version) || !obj.hasOwnProperty('version')) {
        throw new Error('Invalid request header: Invalid Version');
    }

    if (!messageRegexExp.test(obj.message) || !obj.hasOwnProperty('message')) {
        throw new Error('Invalid request header: Invalid Message');
    }

    return obj;
}



// {
//     method: 'GET',
//     uri: 'svn.public.catalog',
//     version: 'HTTP/1.1',
//     message: ''
//   }
