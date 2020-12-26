export function setUserData(data){
  sessionStorage.setItem('auth',JSON.stringify(data));  
};

export function deleteUserData(){
    sessionStorage.removeItem('auth');
  };

export function getUserData(){
    let auth = sessionStorage.getItem('auth');  

    if (auth!=null) {
        return JSON.parse(auth);
    }else{
        return null;
    }
}; 
export function getUserId(){
    let auth = sessionStorage.getItem('auth');  

    if (auth!=null) {
        return JSON.parse(auth).localId;
    }else{
        return null;
    }
}; 
export function objectToArray(data){
    if (data==null) {
        return [];
    }else{
        return Object.entries(data).map(([k,v])=> Object.assign({_id:k},v));
    }
}; 

export async function addPartials(ctx){
    const partials = await Promise.all([
        ctx.load('/templates/common/header.hbs'),
        ctx.load('/templates/common/footer.hbs'),
    ]);

    ctx.partials = {
        header: partials[0],
        footer: partials[1]
    };
}


export const categoryMap = {
    'JavaScript': 'js',
    'C#': 'csharp',
    'Java': 'java',
    'Python': 'python',
};

export function  mapCategories(data) {
    const result = {
        js: [],
        csharp: [],
        java: [],
        python: [],
    };

    for (const article of data) {
        if (article.category!=undefined) {            
            result[categoryMap[article.category]].push(article);
        }
    }

    return result;
}