const assert = require('assert')
const sinon = require('sinon')
const PollingClient = require('../grill/PollingClient')

describe('Polling Client', () => {
    it('Should poll For Grill Status', (done) => {

        const callback = sinon.fake.returns('ok')
        const mock = {
            getGrillStatus() {
                return Promise.resolve(callback())
            }
        }

        let counter = 0
        const logger = sinon.fake()
        const pollingClient = new PollingClient({ client: mock, logger, options: { pollingInterval: 200 } })
        pollingClient.on('status', () => {
            counter++
            if (counter < 3) return

            pollingClient.stop()
            assert(logger.called)
            assert.equal(callback.callCount, counter, 'Polling count')
            done()
        })

        pollingClient.start()
    })
})