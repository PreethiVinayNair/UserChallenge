import selectn from 'selectn';
import _ from 'lodash';

class Path {
    constructor() {
        const args = [...arguments];

        const route = selectn('0.route', args);

        this.route = _.isArray(route)
            ? route
            : [].concat.apply([], args.map(arg => String(arg).split('.')));
    }

    create() {
        return new Path(...this.route, ...arguments);
    }

    parent() {
        return new Path(..._.initial(this.route));
    }

    toString() {
        return this.route.join('.');
    }
}

export default Path;