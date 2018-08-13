const GrillClient = require('cotb-client').GrillClient
const config = require('config')
const options = config.get('grill')
let clientCache

module.exports.initialize = ({ logger }) => {
    clientCache = new GrillClient({
        ...options,
        logger
    })
    return clientCache
}

module.exports.createClient = () => clientCache

