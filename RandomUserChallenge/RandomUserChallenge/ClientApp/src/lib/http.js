import _ from 'lodash';
import {NotificationManager} from 'react-notifications';

function handleError(textContent, onError) {
    if (_.isFunction(onError)) {
        onError(textContent);
        return;
    }

    NotificationManager.error('Request error, check console for details');
    console.error(textContent);
}

export function get(url, onSuccess, onError) {
    (async () => {
        const rawResponse = await fetch(url, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        });

        if (rawResponse.ok) {
            const content = await rawResponse.json();
            onSuccess(content);
            return;
        }

        const content = await rawResponse.text();

        handleError(content, onError);
    })();
}

export function post(url, data, onSuccess, onError) {
    postOrPut('POST', url, data, onSuccess, onError);
}

export function put(url, data, onSuccess, onError) {
    postOrPut('PUT', url, data, onSuccess, onError);
}

function postOrPut(method, url, data, onSuccess, onError) {
    (async () => {
        const rawResponse = await fetch(url, {
            method: method,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        });

        if (rawResponse.ok) {
            const content = await rawResponse.json();
            onSuccess(content);
            return;
        }

        const content = await rawResponse.text();
        handleError(content, onError);
    })();
}

export function del(url, onSuccess, onError) {
    (async () => {
        const rawResponse = await fetch(url, {
            method: 'DELETE',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        });

        if (rawResponse.ok) {
            onSuccess();
            return;
        }

        const content = await rawResponse.text();
        handleError(content, onError);
    })();
}