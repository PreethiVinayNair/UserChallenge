import _ from 'lodash';

export default function (text) {
    return _.trim(text || '').length === 0;
}