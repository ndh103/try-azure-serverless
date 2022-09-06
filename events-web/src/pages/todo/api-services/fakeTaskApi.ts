// eslint-disable-next-line import/no-extraneous-dependencies
import { createServer, Factory, Model, Response } from 'miragejs'
// eslint-disable-next-line import/no-extraneous-dependencies
import { faker } from '@faker-js/faker'
import { TaskStatus } from './taskModel'

const mirageFakeServer = createServer({
  models: {
    task: Model,
  },

  seeds(server) {
    server.createList('task', 10)
  },

  factories: {
    task: Factory.extend({
      id(i) {
        return i
      },

      text() {
        return faker.lorem.sentence()
      },
      status() {
        const statuses = [TaskStatus.InProgress, TaskStatus.Initial]
        return faker.helpers.arrayElements(statuses, 1)
      },
    }),
  },

  routes() {
    this.namespace = 'api'

    this.get(
      '/tasks',
      (schema) => {
        return schema.db.tasks
      },
      { timing: 1000 }
    )

    this.post(
      '/tasks',
      (schema, request) => {
        const attrs = JSON.parse(request.requestBody)

        return schema.db.tasks.insert(attrs)
      },
      { timing: 500 }
    )

    this.post(
      '/tasks/:id',
      (schema, request) => {
        const { id } = request.params
        const attrs = JSON.parse(request.requestBody)

        const task = schema.db.tasks.find((r) => r.id === id)

        if (task) {
          return task.update(attrs)
        }

        return new Response(400, { some: 'header' }, { errors: ['cannot find the item'] })
      },
      { timing: 500 }
    )

    this.delete(
      '/tasks/:id',
      (schema, request) => {
        const { id } = request.params

        const task = schema.db.tasks.find((r) => r.id === id)

        if (task) {
          return task.destroy()
        }

        return new Response(400, { some: 'header' }, { errors: ['cannot find the item'] })
      },
      { timing: 500 }
    )
  },
})

export default mirageFakeServer
