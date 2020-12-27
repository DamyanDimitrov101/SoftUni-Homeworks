import { addPartials, errorHandler, getUserId, mapCategories, notificationHandler } from "../util.js";
import { createDestination, deleteById, editDestination, getAll, getById } from '../data.js';

export async function homePage() {
    await addPartials(this);

    this.partials.destination = await this.load('/templates/catalog/destination.hbs'); /// Additional Partial Of Item

    const data = mapCategories(await getAll());
    
    const context = {};
    context.destinations = data;
    context.user = await  this.app.userData;
    this.partial('/templates/catalog/homePage.hbs', context);
}

export async function createPage(ctx) {
    await addPartials(this);

    const context = {
        user: this.app.userData
    };

    this.partial('/templates/catalog/createPage.hbs', context);
}

export async function postCreate(ctx) {
    const { destination, imgUrl, city, duration, departureDate } = ctx.params; /// PARAMS

    try {
        if (destination.length ==0 ||imgUrl.length ==0 ||departureDate.length ==0||city.length ==0||duration.length ==0) {
            throw new Error('All fields are required!');
        }else if (duration<0 || duration>100) {
            throw new Error('Duration not in the range!');            
        }
         else{
            const context = await createDestination({
                destination,
                imgUrl,
                city,
                duration,
                departureDate
            });

            notificationHandler('Destination added!');
            ctx.redirect('/home');
        }
    } catch (error) {
        errorHandler(error.message);
    }
}

export async function detailsPage() {
    await addPartials(this);
    const destination =await getById(this.params._id);
    
    const context = {
        user: this.app.userData,
        destination: destination,
        canEdit: destination._ownerId == getUserId()
    };
    this.partial('/templates/catalog/detailsPage.hbs', context);
}

export async function editPage() {
    await addPartials(this);
    const destination = await getById(this.params._id);
    if (destination._ownerId !== getUserId()) {
        this.redirect('/home');
    } else {
        const context = {
            user: this.app.userData,
            destination
        };

        this.partial('/templates/catalog/editPage.hbs', context);
    }
}

// POST

export async function postEdit(ctx) {
    const { destination, imgUrl, city,duration,departureDate } = ctx.params; /// PARAMS
    
    try {
        if (destination.length ==0 ||imgUrl.length ==0 ||departureDate.length ==0||city.length ==0||duration.length ==0) {
            throw new Error('All fields are required!');
        }else if (duration<0 || duration>100) {
            throw new Error('Duration not in the range!');            
        }
         else{
            const context = await editDestination(ctx.params._id,{
                destination,
                imgUrl,
                city,
                duration,
                departureDate
            });

            notificationHandler('Successfully edited destination.');
            ctx.redirect(`/details/${ctx.params._id}`);
        }
    } catch (error) {
        errorHandler(error.message);
    }
}

export async function deleteArticle() {    
    try {
        const id = this.params._id;
        const res =await deleteById(id);
        notificationHandler('Destination deleted!');
        this.redirect('/destinations');
    } catch (error) {
        errorHandler(error.message);
    }
}


export async function destinationsPage(ctx) {
    await addPartials(this);

    this.partials.destination = await this.load('/templates/catalog/destinationTicket.hbs'); /// Additional Partial Of Item

    const uid = getUserId();
    const data = mapCategories(await getAll());
    
    let myData = data.filter(d=> d._ownerId == uid);
    const context = {};
    context.destinations = myData;
    context.user = await  this.app.userData;


    this.partial('/templates/catalog/destinationPage.hbs', context);
}