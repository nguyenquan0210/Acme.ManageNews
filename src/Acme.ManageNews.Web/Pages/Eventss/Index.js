$(function () {
    var l = abp.localization.getResource('ManageNews');
    var createModal = new abp.ModalManager(abp.appPath + 'Eventss/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Eventss/EditModal');

    var dataTable = $('#EventssTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(acme.manageNews.catalog.eventss.events.getList),
            columnDefs: [
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('Category'),
                    data: "categoryName"
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
                                        abp.auth.isGranted('ManageNews.Events.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible:
                                        abp.auth.isGranted('ManageNews.Events.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'DeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        acme.manageNews.catalog.eventss.events.delete(data.record.id)
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

    $('#NewEventssButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});