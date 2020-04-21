import _ from 'lodash';
import Path from './Path';

function fetchObject(obj, route) {
    let x = obj;
    _.each(route, key => {
        x = x[key] || newObject(x, key);
    });

    return x;
}

function newObject(parent, key) {
    let obj = {};
    parent[key] = obj;
    return obj;
}

function setValue(obj, property, value) {
    obj[property] = value;
}

function setRouteValue(obj, route, value, part) {
    let o = fetchObject(obj, _.initial(route));

    let property = _.last(route);

    switch (part) {
        case 'delete':
            delete o[property];
            break;
        case 'array-add':
            if (o[property]) {
                o[property].push(value);
            }
            break;
        case 'array-remove':
            if (o[property]) {
                _.remove(o[property], r => _.isEqual(r, value));
            }
            break;
        case 'array-concat':
            if (o[property]) {
                o[property] = o[property].concat(value);
            } else {
                o[property] = value;
            }
            break;
        default:
            setValue(o, property, value);
            break;
    }
}

export default function setValueByPath(obj, path, value, part) {
    let route = new Path(path).route;
    setRouteValue(obj, route, value, part);
}