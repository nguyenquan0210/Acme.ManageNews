$(function () {
    var l = abp.localization.getResource('ManageNews');
    var createModal = new abp.ModalManager(abp.appPath + 'Topics/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Topics/EditModal');

    var dataTable = $('#TopicsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(acme.manageNews.catalog.topics.topic.getList),
            columnDefs: [
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('Hot'),
                    data: "hot"
                },
                {
                    title: l('SortOrder'),
                    data: "sortOrder"
                },
                {
                    title: l('Status'),
                    data: "status",
                    render: function (data) {
                        return l('Enum:Status:' + data);
                    }
                },
                {
                    title: l('Actions'),
                    class: "text-end",
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible:
                                        abp.auth.isGranted('ManageNews.Topics.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible:
                                        abp.auth.isGranted('ManageNews.Topics.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'DeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        acme.manageNews.catalog.topics.topic.delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(
                                                    l('SuccessfullyDeleted')
                                                );
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                }

            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewTopicsButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});